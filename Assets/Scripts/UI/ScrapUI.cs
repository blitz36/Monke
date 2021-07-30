using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrapUI : MonoBehaviour
{
  private playerStatManager PSM;
  public TMP_Text scrapText;
  void Awake()
  {
    if (PSM == null){
      PSM = transform.root.GetComponentInChildren<playerStatManager>();
    }
  }

  void Update() {
    scrapText.text = "Scrap - " + PSM.scrapAmount.ToString();
  }
}
