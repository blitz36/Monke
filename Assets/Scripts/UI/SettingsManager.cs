using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Need this for calling UI scripts

public class SettingsManager : MonoBehaviour {
  private playerStatManager PSM;
  //*********************Variables**********************************
    [SerializeField]
    Transform UIPanel; //Will assign our panel to this variable so we can enable/disable it

    bool isPaused; //Used to determine paused state


  //*********************Variables**********************************
    void Awake()
    {
      if (UIPanel == null)
        UIPanel = GameObject.FindWithTag("SettingsMenu").transform;

      UIPanel.gameObject.SetActive(false); //make sure our pause menu is disabled when scene starts
      isPaused = false; //make sure isPaused is always false when our scene opens
      PSM = transform.root.gameObject.GetComponentInChildren<playerStatManager>();
    }


    void Update()
    {
      //pause or unpause lmao
      if ((PSM.playerInput.Base.Escape.triggered) && !isPaused)
      Pause();

      else if ((PSM.playerInput.Base.Escape.triggered) && isPaused)
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
