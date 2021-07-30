using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldUI : MonoBehaviour
{
    public playerStatManager PSM;
    public List<Image> shieldBar;
    public TMP_Text maxShieldText;
    public TMP_Text currentShieldText;
    public int lastShieldNum;
    void Start()
    {
      if (PSM == null){
        PSM = transform.root.GetComponentInChildren<playerStatManager>();
      }
      updateValues();
    }

    // Update is called once per frame
    void Update()
    {
      if (lastShieldNum != PSM.numShields) {
        lastShieldNum = PSM.numShields;
        updateValues();
      }
    }

    void updateValues() {
      maxShieldText.text = PSM.maxNumShields.Value.ToString();
      currentShieldText.text = PSM.numShields.ToString();
      for (int i = 0; i < lastShieldNum; i++) {
        shieldBar[i].fillAmount = 1;
      }
      for (int i = lastShieldNum; i < PSM.maxNumShields.Value; i++) {
        shieldBar[i].fillAmount = 0;
      }
    }
}
