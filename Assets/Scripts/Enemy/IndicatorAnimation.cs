using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorAnimation : MonoBehaviour
{
  private Material material;

    void Awake() {
      var renderer = transform.GetComponentInChildren<MeshRenderer>();
      material = Instantiate(renderer.sharedMaterial);
      renderer.material = material;
    }

    public void changeScale(float scale) {
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scale);
    }
    public void pulse(){
      material.SetFloat("_PulseSpeed", 100f);
    }

    public void unPulse(){
      material.SetFloat("_PulseSpeed", 0f);
    }
}
