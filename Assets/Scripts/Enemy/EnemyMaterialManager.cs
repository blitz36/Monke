using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaterialManager : MonoBehaviour
{
    private Material material;
    private Color previousColor;
    private EnemyStatManager ESM;
    private bool hitChanged;

    private float currentDissolve = 0f;
    private HitStunState hitstunState;
    private DeadState deadState;

    void Awake() {
      ESM = gameObject.GetComponent<EnemyStatManager>();
      if (hitstunState == null) {
        hitstunState = gameObject.GetComponentInChildren<HitStunState>();
      }

      if (deadState == null) {
        deadState = gameObject.GetComponentInChildren<DeadState>();
      }
    }

    // Start is called before the first frame update
    void Start()
    {
      var renderer = transform.GetChild(0).GetComponent<MeshRenderer>();
      material = Instantiate(renderer.sharedMaterial);
      renderer.material = material;

      previousColor = material.GetColor("_MainColor");
      material.SetColor("_RippleColor", previousColor);
    }

    private void OnDestroy() {
      if(material != null) {
        Destroy(material);
      }
    }

    void Update() {
      if (ESM.SC.currentState == deadState) {
        currentDissolve = currentDissolve - Time.deltaTime/ESM.dissolveTime;
        material.SetFloat("_Dissolve", currentDissolve);
      }

      else if (ESM.SC.currentState == hitstunState) {
        if (hitChanged == false) {
          material.SetColor("_MainColor", Color.red);
          hitChanged = true;
        }
      }

      if (ESM.SC.currentState != hitstunState) {
        if (hitChanged == true) {
          material.SetColor("_MainColor", previousColor);
          hitChanged = false;
        }
      }

    }

}
