using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text fpsDisplay;
    public Text dashDisplay;
    public Text equipDisplay;
    private playerStatManager PSM;

    void Awake(){
      GameObject player = GameObject.FindWithTag("Player");
      PSM = player.GetComponent<playerStatManager>();
    }

    // Update is called once per frame
    void Update()
    {
      float fps = 1/Time.unscaledDeltaTime;
      fpsDisplay.text = "" + fps;
      dashDisplay.text = "Dashes Left:" + PSM.numDashes;
    //  equipDisplay.text = "Charge Available:" + PSM.canUseEquip;
    }
}
