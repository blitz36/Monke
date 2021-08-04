using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndependentHitboxController : MonoBehaviour
{
  public float damage;
  public float momentum;
  public float timeToResume;
  public float timeToResumeSlow;
  private bool stopping;


  void Start(){
  }

    private void OnTriggerEnter(Collider collider) {
      if (collider.tag == "Enemy")
      {
        EnemyStatManager ESM = collider.transform.GetComponent<EnemyStatManager>();
        ESM.TakeDamage(damage);
        var moveDirection = transform.position - collider.transform.position;
        ESM.rb.AddForce(moveDirection.normalized * momentum, ForceMode.Impulse);

      }
    }


}
