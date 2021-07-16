using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaterialManager : MonoBehaviour
{
    private Material material;
    private Color previousColor;
    private EnemyStatManager est;
    private bool hitChanged;

    private float currentDissolve = 0f;

    void Awake() {
      est = gameObject.GetComponent<EnemyStatManager>();
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
      if (est.isHit == true) {
        if (hitChanged == false) {
          material.SetColor("_MainColor", Color.red);
          hitChanged = true;
        }
      }
      else if (est.isHit == false) {
        if (hitChanged == true) {
          material.SetColor("_MainColor", previousColor);
          hitChanged = false;
        }
      }

      if (est.isDie == true) {
        currentDissolve = currentDissolve + Time.deltaTime/est.dissolveTime;
        material.SetFloat("_Dissolve", currentDissolve);
      }
    }
}
