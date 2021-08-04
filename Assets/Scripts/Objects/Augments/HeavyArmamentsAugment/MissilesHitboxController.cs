using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilesHitboxController : MonoBehaviour
{
  public Transform target;
  private Rigidbody rb;

  public float turnSpeed = 1f;
  public float rocketSpeed = 10f;

  public float damage;
  public float momentum;
  public float missileRange = 20f;
  public LayerMask layerMask;

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
    rb.velocity = transform.forward * rocketSpeed;
    Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
    rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed));
  }

  private void OnTriggerEnter(Collider collider) {
    if (collider.tag == "Enemy")
    {
      EnemyStatManager ESM = collider.transform.GetComponent<EnemyStatManager>();
      ESM.TakeDamage(damage);
      var moveDirection = transform.position - collider.transform.position;
      ESM.rb.AddForce(moveDirection.normalized * momentum, ForceMode.Impulse);
      Destroy(gameObject);
    }
  }


}
