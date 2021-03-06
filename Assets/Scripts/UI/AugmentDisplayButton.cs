using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AugmentDisplayButton : MonoBehaviour
{
  public TMP_Text Name;
  public TMP_Text Description;
  public TMP_Text FlavorText;
  public Image icon;
  public int stackCount;
  public TMP_Text stackText;
  public GameObject infoOverlay;

  void Awake() {
    infoOverlay.SetActive(false);
  }

  public void updateText(Item item) {
    Name.text = item.name;
    Description.text = item.description;
    FlavorText.text = item.FlavorText;
    icon.sprite = item.icon;
    stackText.text = stackCount.ToString() + "x";
  }

  public void OverLayOn()
  {
      infoOverlay.SetActive(true);
  }

  public void OverLayOff() {
    infoOverlay.SetActive(false);
  }
}
