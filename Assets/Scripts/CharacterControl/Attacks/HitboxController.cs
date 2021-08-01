using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
  public float damage;
  private float flameDamage;
  public float momentum;
  public float timeToResume;
  public float timeToResumeSlow;
  private bool stopping;
  public playerStatManager PSM;

  public delegate void AugmentedHitboxFunc(EnemyStatManager ESM, playerStatManager PSM);
  public AugmentedHitboxFunc augmentedHitboxFunc;


  void Start(){
    PSM = transform.root.GetComponentInChildren<playerStatManager>();
    Debug.Log(transform.root);
  }

    private void OnTriggerEnter(Collider collider) {
      if (collider.tag == "Enemy")
      {
        Debug.Log(collider);
        EnemyStatManager ESM = collider.transform.GetComponent<EnemyStatManager>();
        ESM.TakeDamage(damage);
        StartCoroutine(PSM.Stop(timeToResume, timeToResumeSlow));
        var moveDirection = transform.position - collider.transform.position;
        ESM.rb.AddForce(moveDirection.normalized * momentum, ForceMode.Impulse);
        if (augmentedHitboxFunc != null)
          augmentedHitboxFunc(ESM, PSM);
      }
    }

    public virtual void updateDamageValue(float newDamage) {
      damage = newDamage;
    }

}
