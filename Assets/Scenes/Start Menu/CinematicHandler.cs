using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CinematicHandler : MonoBehaviour
{
  public TMP_Text SkipText;

  public void OnEnable() {
      SkipText.text = "PRESS SPACE TO CONTINUE";
    }
}
