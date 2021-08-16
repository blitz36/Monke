using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScrap : PickupableItems
{
  public bool isInPickupRange;
  public float pickUpRange;
  [SerializeField] private LayerMask playerLayer;
  public Rigidbody rb;
  public Transform target;
  public float autoPickupTime;
  void Awake(){
    rb = gameObject.GetComponent<Rigidbody>();
    target = GameObject.FindWithTag("Player").transform;

  }
  void Start() {
    StartCoroutine(autoPickup());
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player") {
        playerStatManager PSM = other.gameObject.GetComponent<playerStatManager>();
        PSM.scrapAmount += 10f;
        Destroy(gameObject);
      }

  }

    public override void pickup(){

    }

    public IEnumerator autoPickup() {
      yield return new WaitForSeconds(autoPickupTime);
      transform.position = target.position;
    }

    void FixedUpdate(){
      if (isInPickupRange == false) {
        isInPickupRange = Physics.CheckSphere(transform.position, pickUpRange, playerLayer);
      }
      if (isInPickupRange) {
        Vector3 direction = target.position - transform.position;
        rb.AddRelativeForce(direction * 1000f * Time.deltaTime, ForceMode.Force);
      }
    }

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.green;
      //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
      Gizmos.DrawWireSphere (transform.position, pickUpRange);
    }
}
