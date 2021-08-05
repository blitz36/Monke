using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
  public float damage;
  public float momentum;
  public float timeToResume;
  public float timeToResumeSlow;

  public float timeToResumeCrit;
  public float timeToResumeSlowCrit;

  private bool stopping;
  public playerStatManager PSM;

  public delegate void AugmentedHitboxFunc(EnemyStatManager ESM, playerStatManager PSM);
  public AugmentedHitboxFunc augmentedHitboxFunc;


  void Start(){
    PSM = transform.root.GetComponentInChildren<playerStatManager>();
  }

    private void OnTriggerEnter(Collider collider) {
      if (collider.tag == "Enemy")
      {
        EnemyStatManager ESM = collider.transform.GetComponent<EnemyStatManager>();
        float roll = Random.value;
        if (roll < PSM.critChancePerc.Value) {//if it is a critical strike
          Debug.Log(roll + " " + PSM.critChancePerc.Value);
          ESM.TakeDamage(damage*2f);
          StartCoroutine(PSM.Stop(timeToResumeCrit, timeToResumeSlowCrit));
          var moveDirection = transform.position - collider.transform.position;
          ESM.rb.AddForce(moveDirection.normalized * momentum, ForceMode.Impulse);
        }
        else {//not critical strike
          ESM.TakeDamage(damage);
          StartCoroutine(PSM.Stop(timeToResume, timeToResumeSlow));
          var moveDirection = transform.position - collider.transform.position;
          ESM.rb.AddForce(moveDirection.normalized * momentum, ForceMode.Impulse);
        }

        if (augmentedHitboxFunc != null) {
          augmentedHitboxFunc(ESM, PSM);
        }
      }
    }

    public virtual void updateDamageValue(float newDamage) {
      damage = newDamage;
    }

}
