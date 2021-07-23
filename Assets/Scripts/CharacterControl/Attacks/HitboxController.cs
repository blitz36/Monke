using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
  public float damage;
  public float momentum;
  public float timeToResume;
  public float timeToResumeSlow;
  private bool stopping;
  public playerStatManager PSM;

  void Start(){
    PSM = transform.parent.GetComponentInChildren<playerStatManager>();
    Debug.Log(transform.parent);
  }

    private void OnTriggerEnter(Collider collider) {
      if (collider.tag == "Enemy")
      {
        Debug.Log(collider);
        EnemyStatManager est = collider.transform.GetComponent<EnemyStatManager>();
        est.TakeDamage(damage);
        PSM.currentHealth += PSM.lifeSteal.Value;
        var moveDirection = transform.position - collider.transform.position;
        est.rb.AddForce(moveDirection.normalized * momentum, ForceMode.Impulse);
      }
    }

    public virtual void updateDamageValue(float newDamage) {
      damage = newDamage;
    }

    public void stopEffect(){
      if (!stopping) {
        StartCoroutine("Stop");
        Time.timeScale = 0f;
      }
    }

    IEnumerator Stop() {
      yield return new WaitForSecondsRealtime(timeToResume);
      Time.timeScale = 0.05f;
      yield return new WaitForSecondsRealtime(timeToResumeSlow);
      Time.timeScale = 1f;
    }
}
