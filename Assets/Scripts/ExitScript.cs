using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
  public Text leaveDescription;
  public int nextScene;

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
  //  if (PSM.playerInput.Base.Interact.triggered) {
    //  SceneManager.LoadScene(nextScene);
    //}
  }


}
