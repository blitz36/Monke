using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
  public float damage;
  public float momentum;

    private void OnTriggerEnter(Collider collider) {
      if (collider.tag == "Enemy")
      {
        EnemyStatManager est = collider.transform.GetComponent<EnemyStatManager>();
        est.TakeDamage(damage);
        var moveDirection = transform.position - collider.transform.position;
        est.rb.AddForce(moveDirection.normalized * momentum, ForceMode.Impulse);
      }
    }

    public virtual void updateDamageValue(float newDamage) {
      damage = newDamage;
    }
}
