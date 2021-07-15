using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerHitboxSensor : MonoBehaviour
{
  public bool isDanger = false;
  void OnTriggerEnter(Collider col)
  {
    isDanger = true;
    Debug.Log("TRUU");
  }

  void OnTriggerExit(Collider col) {
    isDanger = false;
  }
  void FixedUpdate(){
    //whatever you are doing for movement
  //  isDanger = false;
  }

}
