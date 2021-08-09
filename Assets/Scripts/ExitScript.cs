using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
  public Text leaveDescription;
  public int nextScene;
  public bool isShop = false;

  void Start() {
    leaveDescription.gameObject.SetActive(false);
  }

  void OnTriggerEnter(Collider col) {
    leaveDescription.gameObject.SetActive(true);
  }

  void OnTriggerExit(Collider col) {
    leaveDescription.gameObject.SetActive(false);
  }

  public void leaveMap(playerStatManager PSM) {
    PSM.currentMap = nextScene;
    if (isShop == true) {
      PSM.inShop = true;
      SceneManager.LoadScene(4);
    }
    else {
      PSM.inShop = false;
      SceneManager.LoadScene(nextScene);
    }
  }


}
