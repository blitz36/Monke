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
  public GameObject overlay;

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
        overlay.SetActive(false);
      }
      else {
        if (hasTimeReset == false) {
          overlay.SetActive(true);
          hasTimeReset = true;
          timeValue = playerEquipState.equipCDTime;
        }
        timeValue -= Time.deltaTime;
        cooldownNumber.text = timeValue.ToString("0.00");
        cooldownOutline.fillAmount = 1- timeValue/playerEquipState.equipCDTime;
      }
    }

    void decrementNumber() {
    }
}
