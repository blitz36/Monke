using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipCooldownUI : MonoBehaviour
{
  public playerStatManager PSM;
  public Image cooldownOutline;
  public TMP_Text cooldownNumber;
  public float timeValue;
  public bool hasTimeReset = false;
  public PlayerEquipState playerEquipState;

    void Awake()
    {
      if (PSM == null){
        PSM = transform.root.GetComponentInChildren<playerStatManager>();
      }
    }

    // Update is called once per frame
    void Update()
    {
      if (playerEquipState.canUseEquip) {
        cooldownNumber.text = "";
        hasTimeReset = false;
      }
      else {
        if (hasTimeReset == false) {
          hasTimeReset = true;
          timeValue = playerEquipState.equipCDTime;
        }
        timeValue -= Time.deltaTime;
        cooldownNumber.text = (Mathf.Round(timeValue* 100.0f) * 0.01f).ToString();
        cooldownOutline.fillAmount = timeValue/playerEquipState.equipCDTime;
      }
    }

    void decrementNumber() {
    }
}
