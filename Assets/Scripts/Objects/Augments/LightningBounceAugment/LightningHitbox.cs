using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningHitbox : MonoBehaviour
{
  public int numBounces;
  public int bounceCounter = 0;
  public LayerMask layerMask;
  public float lightningRange;

  public Transform target;
  private Rigidbody rb;

  public float turnSpeed = 10f;
  public float lightningSpeed = 10f;

  public float damage;
  public float momentum;
  public float missileRange = 20f;

  void Awake() {
    if (rb == null) {
      rb = gameObject.GetComponent<Rigidbody>();
    }
  }

  void FixedUpdate() {
    if (target == null) {
      Destroy(gameObject);
      return;
    }
    rb.velocity = transform.forward * lightningSpeed;
    Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
    rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed));
  }

  private void OnTriggerEnter(Collider collider) {
    if (collider.tag == "Enemy")
    {
      EnemyStatManager ESM = collider.transform.GetComponent<EnemyStatManager>();
      ESM.TakeDamage(damage);

      bounceCounter += 1;
      Collider[] hitColliders = Physics.OverlapSphere(transform.position, lightningRange, layerMask);
      Collider thisCollider = gameObject.GetComponent<Collider>();
      target = hitColliders[Random.Range(0, hitColliders.Length)].transform;
      while (target == gameObject.transform) {
        target = hitColliders[Random.Range(0, hitColliders.Length)].transform;
      }
      if (bounceCounter == numBounces) {
        Destroy(gameObject);
      }
    }
  }


}
