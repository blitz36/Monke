using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public float updateSpeedSeconds;
    public Image slider;
    public Image sliderWhite;
    public Image sliderBlue;
    public Image sliderYellow;

    public void SetMaxShield(float shield) {
      sliderBlue.fillAmount = 1;
      sliderYellow.fillAmount = 1;
    }

    public void SetMaxHealth(float health)
    {
        slider.fillAmount = 1;
        sliderWhite.fillAmount = 1;
    }

    public void SetShield(float percentShield)
    {
      if (sliderBlue == null) return;
      StartCoroutine(ChangeSlider(percentShield, sliderBlue, sliderYellow));
      sliderBlue.fillAmount = percentShield;
    }

    public void SetHealth(float percentHealth)
    {
      if (slider == null) return;
      StartCoroutine(ChangeSlider(percentHealth, slider, sliderWhite));
      slider.fillAmount = percentHealth;
    }

    private IEnumerator ChangeSlider(float amt, Image sliderPrimary, Image sliderSecondary) {
      float preChangeAmt = sliderPrimary.fillAmount;
      sliderSecondary.fillAmount = preChangeAmt;
      float elapsed = 0f;

      while (elapsed < updateSpeedSeconds) {
        if (sliderSecondary == null) yield break;
        elapsed += Time.deltaTime;
        sliderSecondary.fillAmount = Mathf.Lerp(preChangeAmt, amt, elapsed/updateSpeedSeconds);
        yield return null;
      }

      sliderSecondary.fillAmount = amt;
    }

}
