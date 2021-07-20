using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXActivate : MonoBehaviour
{
    private VisualEffect VFX;

    void Awake() {
      VFX = gameObject.GetComponent<VisualEffect>();
    }

    public void playVFX() {
      VFX.Play();
    }
}
