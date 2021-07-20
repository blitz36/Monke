using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public float updateSpeedSeconds;
    public Slider slider;
    public Slider sliderWhite;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        sliderWhite.maxValue = health;
        sliderWhite.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
        StartCoroutine(ChangeSlider(health));
    }

    private IEnumerator ChangeSlider(float amt) {
      float preChangeAmt = sliderWhite.value;
      float elapsed = 0f;

      while (elapsed < updateSpeedSeconds) {
        elapsed += Time.deltaTime;
        sliderWhite.value = Mathf.Lerp(preChangeAmt, amt, elapsed/updateSpeedSeconds);
        yield return null;
      }

      sliderWhite.value = amt;
    }

}
