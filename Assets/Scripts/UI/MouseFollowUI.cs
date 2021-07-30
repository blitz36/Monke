using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseFollowUI : MonoBehaviour
{
  public playerStatManager PSM;
  public TMP_Text equipText;
  public TMP_Text lightComboText;
  public TMP_Text dashText;
  public TMP_Text chargeAttackText;
  public Image equipFill;
  public Image lightComboFill;
  public Image dashFill;
  public Image chargeFill;

    void Awake() {
      if (PSM == null){
        PSM = transform.root.GetComponentInChildren<playerStatManager>();
      }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
        lightComboText.text = PSM.comboStep.ToString();
        dashText.text = PSM.numDashes.ToString();
        if (PSM.dashCDTimer == 0) {
          dashFill.fillAmount = 0f;
        }
        else {
          dashFill.fillAmount = 1f - PSM.dashCDTimer/PSM.dashCooldown;
        }
    }
}
