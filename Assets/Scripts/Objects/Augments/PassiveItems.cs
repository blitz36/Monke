using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : ScriptableObject
{
    private bool trail = false;
    public GameObject trailPiece;
    void Update()
    {
      if (trail == false) {

      }
    }

    void OnTriggerEnter(Collider collider) {
      if (collider.tag == "trail") {
        trail = true;
      }
    }
}
