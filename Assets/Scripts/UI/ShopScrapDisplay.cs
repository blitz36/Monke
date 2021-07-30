using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScrapDisplay : MonoBehaviour
{
  public TMP_Text scrapText;
  private playerStatManager PSM;

  void Awake() {
    PSM = GameObject.FindWithTag("Player").transform.root.GetComponentInChildren<playerStatManager>();
  }

  void Update(){
    scrapText.text = PSM.scrapAmount.ToString();
  }
}
