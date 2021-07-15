using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MovementAgent : MonoBehaviour
{
    public float speed;
    public int rng;
    public Transform targetPosition; //the target to move towards

    public int numDanger;

    public AStarPath AStar;
    private Rigidbody rb;

    //context based steering
    public int numRays; //number of directions ai can go
    private float[] rayDirections; //stores angle of each direction

    public float[] interest;

    private Vector3 chosenDirection;
    private Vector3 velocity;

    //detecting undesirable directions
    public List<DangerHitboxSensor> Sensor;
    public List<GameObject> dangerHitbox;
    private bool[] danger;
    public float lookDistance;
    private int layerMask = 1 << 12;

    //relating towards shaping the weights for different behaviors
    public int directionCache;
    public bool inRange = false;

    //sqrLen is square length easier computationally then doing distance because of sqr call
    public float stopDistance;
    public float sqrLen;

    void Awake(){
      if (targetPosition == null)
        targetPosition = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
    //  InvokeRepeating("randomSeed", 5.0f, 5.0f); //this is to have randomized things, but not in use

      //set up the various arrays for context based steering
      rayDirections = new float[numRays];
      interest = new float[numRays];
      danger = new bool[numRays];

      //retrieving components to use
      AStar = GetComponent<AStarPath>();
      rb = GetComponent<Rigidbody>();

      //initializing  each angle that can be moved towards
      for(int i = 0; i < numRays; i++) {
        float angle = i*360/numRays;
        rayDirections[i] = angle;
        dangerHitbox[i].transform.localRotation = Quaternion.Euler(0, angle - 90, -90);
        Sensor[i] = dangerHitbox[i].transform.GetChild(0).GetComponent<DangerHitboxSensor>();
      }

    }

    public void moveToPlayer() {
      //Calculate how far away the target is for weight calculations
      sqrLen = ((targetPosition.position - transform.position).sqrMagnitude)/stopDistance;
      if (sqrLen < 1f) {
        inRange = true;
      }
      else if (sqrLen > 1.5f) {
        inRange = false;
      }
      numDanger = 0;
      //find the context to inform the steering
      setInterest();
      setDanger();
      chosenDirection = chooseDirection();

      //Move with Rigidbody in chosen direction
      velocity = chosenDirection * speed;
      velocity.y = rb.velocity.y;
      rb.velocity = velocity;
    }

    void setInterest() {
      //find the best path route through a*, then take the dot product with each direction in order to see which
      //directions are the most desirable. The closer to 1 the dot product is, the more desirable.
      Vector3 pathDir = AStar.calculateDir();
      for (int i = 0; i < numRays; i++) {
        float dp = Vector3.Dot(pathDir, Quaternion.Euler(0, rayDirections[i], 0) * transform.forward);
        if (i == 0) {
        }
        interest[i] = dp;
      }
    }

    void setDanger() {
      //raycast in each direction to the distance of the look distance. If it hits anything in the enemy layer, then that area is undesirable to move to so set danger there to true.
      for (int i = 0; i < numRays; i++) {
        if (Sensor[i].isDanger == true)
          {
      //      Debug.DrawRay(transform.position, Quaternion.Euler(0, dangerHitbox[i].transform.localRotation.y, 0) * transform.forward, Color.red);
            danger[i] = true;
            numDanger += 1;
        }
        else {
          danger[i] = false;
        }
      }
    }

    Vector3 chooseDirection() {
      //Given the interest dot product values, weights are then calculated further for ai behavior. Then the danger zones are cancelled out, and then the highest weights
      //value is chosen from there as the direction to go. Return the chosen direction
      Vector3 chosenDir = new Vector3(0f, 0f, 0f);
      //calculate weights for all directions
      for (int i = 0; i < numRays; i++) {
        interest[i] = calculateWeight(interest[i], i);
        Debug.DrawRay(transform.position, Quaternion.Euler(0, rayDirections[i], 0) * transform.forward * 2 * interest[i], Color.green);
      }

      //cancel out danger zones
      for (int i = 0; i < numRays; i++) {
        if (danger[i] == true) {
          interest[i] = 0f;
          Debug.DrawRay(transform.position, Quaternion.Euler(0, rayDirections[i], 0) * transform.forward * 2, Color.red);
        }
      }

      int directionIndex = maxIndex(); //get index of highest value
      directionCache = directionIndex; //cache the last direction moved for weight stuff

      //get direction to go to and return it
      chosenDir = Quaternion.Euler(0, rayDirections[directionIndex], 0) * transform.forward;
      chosenDir = chosenDir.normalized;
      Debug.DrawRay(transform.position, Quaternion.Euler(0, rayDirections[directionIndex], 0) * transform.forward*4*interest[directionIndex], Color.black);
      return chosenDir;
    }

    //weight calculation section. Under certain conditions the weights change.
    float calculateWeight(float interestDP, int index) {
      float weight = interestDP;

      //if in range then instead of moving towards the target, strafe side to side. values nearer to 0 is more valued which is the dot product of two perpendicular vectors
      //then reduce the weights to allow for other emergent behaviors the closer it is to the target
      if (inRange == true) {
        weight = 1.0f - Mathf.Abs(weight);
        weight *= sqrLen/2;
      }

      //To encourage more avoiding behavior, weights are added if there is danger detected in a direction. The opposite direction has its weights increased then to
      //prefer avoiding enemies. Then a shaping function is done which will favor moving angled rather than direction back, to give smoother movement.

      if (numDanger < numRays/3) {
        if (danger[index] == true) {
          for (int j = 0; j < numRays; j++) {
            float dp = (Vector3.Dot(Quaternion.Euler(0, rayDirections[index], 0) * transform.forward, Quaternion.Euler(0, rayDirections[j], 0) * transform.forward))*-1f;
            dp = Mathf.Max(0, dp);
            if (dp > 0) {
              dp = 1.0f - Mathf.Abs(dp - 0.65f);
            }
            dp *= 0.15f;
            interest[j] += dp;
          }
        }
      }
      //As two sides will have exactly the same dot product, there must be a way to chose one over another. As a result this is made
      //The side that has gone last will be more likely to be chosen. This is done by making everything on the opposite side lessed
      //so that in the case that there are no other options, the other direction can still be taken.
      //As the array is a circle and going over or under should loop it, I had to write out this code in order to maintain that logic
      //the way i visualized this is as a bar with two segments, the segment thats the same side and that where its the opposite side.
      //sliding the each segment too high or low will loop it around. This logic makes the opposite side less.
      bool overflow = false;
      if (directionCache + numRays/4 >= numRays || directionCache - numRays/4 < 0) {
        overflow = true;
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

//formula to find indeex
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

//modulo cuz apparently c# does not have modulo
    int mod(int x, int m) {
        int r = x%m;
        return r<0 ? r+m : r;
    }

    void randomSeed(){
      rng = Random.Range(1, 5);
    }
}
