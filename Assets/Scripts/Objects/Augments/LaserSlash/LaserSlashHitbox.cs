using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSlashHitbox : MonoBehaviour
{

  Rigidbody rb;
  public float momentum;
  public float lifespanTime;

  void Awake(){
    rb = gameObject.GetComponent<Rigidbody>();
  }

  void OnEnable()
   {
     rb.velocity = transform.forward * momentum;
     StartCoroutine(lifespan());
   }

   IEnumerator lifespan() {
     yield return new WaitForSeconds(lifespanTime);
     gameObject.SetActive(false);
   }
}
