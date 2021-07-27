using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHitbox : MonoBehaviour
{
    public float releaseTime;

    private void OnTriggerStay(Collider collider) {
      if (collider.tag == "Enemy")
      {
        releaseTime += Time.deltaTime;
      }
    }

    private void OnTriggerExit(Collider other) {
      releaseTime = 0f;
    }

    private void OnEnable() {
      releaseTime = 0f;
    }
}
