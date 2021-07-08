using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
  public float damage;

    private void OnTriggerEnter(Collider collider) {
      EnemyStatManager est = collider.transform.GetComponent<EnemyStatManager>();
      est.TakeDamage(damage);
    }

    public void updateDamageValue(float newDamage) {
      damage = newDamage;
    }
}
