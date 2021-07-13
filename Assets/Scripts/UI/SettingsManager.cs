using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Need this for calling UI scripts

public class SettingsManager : MonoBehaviour {
  //*********************Variables**********************************
    [SerializeField]
    Transform UIPanel; //Will assign our panel to this variable so we can enable/disable it

    bool isPaused; //Used to determine paused state


  //*********************Variables**********************************
    void Start()
    {
      if (UIPanel == null)
        UIPanel = GameObject.FindWithTag("SettingsMenu").transform;

      UIPanel.gameObject.SetActive(false); //make sure our pause menu is disabled when scene starts
      isPaused = false; //make sure isPaused is always false when our scene opens
    }


    void Update()
    {
      //pause or unpause lmao
      if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
      Pause();

      else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
      UnPause();
    }

    void Pause() {
      isPaused = true;
      UIPanel.gameObject.SetActive(true);
      Time.timeScale = 0f;
    }

    void UnPause() {
      isPaused = false;
      UIPanel.gameObject.SetActive(false);
      Time.timeScale = 1f;
    }

}
