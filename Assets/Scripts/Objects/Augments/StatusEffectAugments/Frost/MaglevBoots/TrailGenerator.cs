using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGenerator : MonoBehaviour
{
    public GameObject trail;
    public bool onTrail = false;

    void FixedUpdate(){
      if (onTrail == false) {
        Instantiate(trail, transform.position, transform.rotation);
      }

      onTrail = false;
    }

    public void OnTriggerStay(Collider collider) {
      TrailType type = collider.GetComponent<TrailType>();
      if (type == null) {
        return;
      }
      onTrail = true;


    }

  /*  public void OnTriggerExit(Collider collider) {
      TrailType type = collider.GetComponent<TrailType>();
      if (type == null) {
        return;
      }
      onTrail = false;
    }*/
}
