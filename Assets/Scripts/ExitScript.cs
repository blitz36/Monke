using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExitScript : MonoBehaviour
{
  public Text leaveDescription;

  void Start() {
    leaveDescription.gameObject.SetActive(false);
  }

  void OnTriggerEnter(Collider col) {
    leaveDescription.gameObject.SetActive(true);
  }

  void OnTriggerExit(Collider col) {
    leaveDescription.gameObject.SetActive(false);
  }

  void OnTriggerStay(Collider Collider) {
    if (Input.GetKeyDown("r")) {
      Application.LoadLevel(Application.loadedLevel);
    }
  }


}
