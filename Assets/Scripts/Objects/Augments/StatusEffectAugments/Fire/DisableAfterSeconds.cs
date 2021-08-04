using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterSeconds : MonoBehaviour
{
  public float disableTime;
    void OnEnable() {
      StartCoroutine(inActive());
    }

    IEnumerator inActive() {
      yield return new WaitForSeconds(disableTime);
      gameObject.SetActive(false);
    }
}
