using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxController : MonoBehaviour
{
  public float damage = 10f;
  public float momentum;

    private void OnTriggerEnter(Collider collider) {
    //  if (collider.tag == "Player")
    //  {
        playerStatManager pst = collider.transform.GetComponent<playerStatManager>();
        pst.TakeDamage(damage);
        var moveDirection = transform.position - collider.transform.position;
    //  }
  //    pst.rb.AddForce(moveDirection.normalized * momentum, ForceMode.Impulse);
    }

    public virtual void updateDamageValue(float newDamage) {
      damage = newDamage;
    }
}