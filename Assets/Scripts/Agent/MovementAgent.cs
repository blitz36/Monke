using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MovementAgent : MonoBehaviour
{
    public int rng;
    public float test;
    public Transform targetPosition;

    //encircling instead of going towards
    public float stopDistance;
    public float sqrLen;
  //context based steering
    public float speed;
    public float lookDistance;
    public int numRays;

    private int layerMask = 1 << 11;

    private float[] rayDirections;
    public float[] interest;
    private bool[] danger;
    public int directionCache;
    public bool inRange = false;

    private Vector3 chosenDirection;
    private Vector3 velocity;

    public AStarPath AStar;
    private CharacterController controller;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
      InvokeRepeating("randomSeed", 5.0f, 5.0f);
      rayDirections = new float[numRays];
      interest = new float[numRays];
      danger = new bool[numRays];
      AStar = GetComponent<AStarPath>();
      controller = GetComponent<CharacterController>();
      rb = GetComponent<Rigidbody>();

      for(int i = 0; i < numRays; i++) {
        float angle = i*360/numRays;
        rayDirections[i] = angle;
      }

    }

    void FixedUpdate() {
      setInterest();
      setDanger();
      sqrLen = ((targetPosition.position - transform.position).sqrMagnitude)/stopDistance;
      if (sqrLen < 1f) {
        inRange = true;
      }
      else if (sqrLen > 1.5f) {
        inRange = false;
      }
      chosenDirection = chooseDirection();
      velocity = chosenDirection * speed;
    //  rb.AddForce(velocity);
      rb.velocity = velocity;
    }

    void setInterest() {
      Vector3 pathDir = AStar.calculateDir();
      for (int i = 0; i < numRays; i++) {
        float dp = Vector3.Dot(pathDir, Quaternion.Euler(0, rayDirections[i], 0) * transform.forward);
        if (i == 0) {
        //  Debug.Log("DIRECTION :" + Quaternion.Euler(0, rayDirections[i], 0)*transform.forward + "|||| DP: " + dp);

        }
        interest[i] = Mathf.Max(0, dp);
        interest[i] = dp;
      }
    }

    void setDanger() {
      for (int i = 0; i < numRays; i++) {
      //  if (Physics.Raycast(transform.position, Quaternion.Euler(0, rayDirections[i], 0) * transform.forward, lookDistance, layerMask))
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.5f, Quaternion.Euler(0, rayDirections[i], 0) * transform.forward, out hit, lookDistance, layerMask))
          {
            Debug.DrawRay(transform.position, Quaternion.Euler(0, rayDirections[i], 0) * transform.forward, Color.red);
            danger[i] = true;
        }
        else {
          danger[i] = false;
        }
      }
    }

    Vector3 chooseDirection() {
      Vector3 chosenDir = new Vector3(0f, 0f, 0f);
      for (int i = 0; i < numRays; i++) {
        interest[i] = calculateWeight(interest[i], i);
        Debug.DrawRay(transform.position, Quaternion.Euler(0, rayDirections[i], 0) * transform.forward * 2 * interest[i], Color.green);
      }
      for (int i = 0; i < numRays; i++) {
        if (danger[i] == true) {
          interest[i] = 0f;
        }
      }
      int directionIndex = maxIndex();
      directionCache = directionIndex;
      chosenDir = Quaternion.Euler(0, rayDirections[directionIndex], 0) * transform.forward;
      chosenDir = chosenDir.normalized;
      Debug.DrawRay(transform.position, Quaternion.Euler(0, rayDirections[directionIndex], 0) * transform.forward*4*interest[directionIndex], Color.black);
      return chosenDir;
    }

    void randomSeed(){
      rng = Random.Range(1, 5);
    }

    float calculateWeight(float interestDP, int index) {
      float weight = interestDP;
      if (inRange == true) {
        weight = 1.0f - Mathf.Abs(weight);
        weight *= sqrLen/2;
      }


      bool overflow = false;
      if (directionCache + numRays/4 >= numRays || directionCache - numRays/4 < 0) {
        overflow = true;
      }

      if (danger[index] == true) {
        for (int j = 0; j < numRays; j++) {
          float dp = (Vector3.Dot(Quaternion.Euler(0, rayDirections[index], 0) * transform.forward, Quaternion.Euler(0, rayDirections[j], 0) * transform.forward))*-1f;
          dp = Mathf.Max(0, dp);
          if (dp > 0) {
            dp = 1.0f - Mathf.Abs(dp - 0.65f);
          }
          dp *= 0.06f;
          interest[j] += dp;
        }
      }

      if (index < mod(directionCache+numRays/4, numRays) || index > mod(directionCache-numRays/4, numRays)) {
        if (overflow == false) {
          if (index > mod(directionCache+numRays/4, numRays) || index < mod(directionCache-numRays/4, numRays)) {
            weight *= 0.5f;
          }
        }
      }
      else {
        weight *= 0.5f;
      }
      return weight;
    }

    int maxIndex() {
      int index = 0;
      float maxWeight = -1f;
      for (int i = 0; i < numRays; i++) {
        if (maxWeight < interest[i] ) {
          maxWeight = interest[i];
          index = i;
        }
      }
      return index;
    }

    int mod(int x, int m) {
        int r = x%m;
        return r<0 ? r+m : r;
    }
}
