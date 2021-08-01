using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public float updateSpeedSeconds;
    public Image slider;
    public Image sliderWhite;

    public void SetMaxHealth(float health)
    {
        slider.fillAmount = 1;
        sliderWhite.fillAmount = 1;
    }
    public void SetHealth(float percentHealth)
    {
      if (slider == null) return;
        slider.fillAmount = percentHealth;
        StartCoroutine(ChangeSlider(percentHealth));
    }

    private IEnumerator ChangeSlider(float amt) {
      float preChangeAmt = sliderWhite.fillAmount;
      float elapsed = 0f;

      while (elapsed < updateSpeedSeconds) {
        if (sliderWhite == null) yield break;
        elapsed += Time.deltaTime;
        sliderWhite.fillAmount = Mathf.Lerp(preChangeAmt, amt, elapsed/updateSpeedSeconds);
        yield return null;
      }

      sliderWhite.fillAmount = amt;
    }

}
