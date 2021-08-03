using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultipleMaterialManager : MonoBehaviour
{
    public GameObject Geometry;
    private List<Material> material = new List<Material>();
    private List<Color> previousColor = new List<Color>();
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
      int index = 0;
      //get materials of every child object and get their color
      foreach (Transform child in Geometry.transform) {
        var renderer = child.GetComponent<SkinnedMeshRenderer>();

        int j = 0;
        foreach (Material shared in renderer.sharedMaterials) { //some child objects have multiple materials
          Material newMaterial = Instantiate(shared);
          if (!newMaterial.HasProperty("_MainColor")) { //cant change color if there isnt one
            j += 1;
            continue;
          }

          var color = newMaterial.GetColor("_MainColor");
          material.Add(newMaterial);
          //make copy of materials list, change it, and reset it
          var mats = renderer.sharedMaterials;
          mats[j] = newMaterial;
          renderer.sharedMaterials = mats;

          previousColor.Add(color);
          newMaterial.SetColor("_RippleColor", color); // idunno what this is lmao
          index += 1;
          j += 1;
        }
      }
    }

    private void OnDestroy() {
      if(material != null) {
        foreach (Material mat in material) {
          Destroy(mat);
        }
      }
    }

    void Update() {
      if (ESM.SC.currentState == deadState) {
        foreach (Material mat in material) {
          currentDissolve = currentDissolve - Time.deltaTime/ESM.dissolveTime;
          mat.SetFloat("_Dissolve", currentDissolve);
          mat.SetFloat("_DissolveEmis", currentDissolve);
        }
      }

      else if (ESM.SC.currentState == hitstunState) {
        if (hitChanged == false) {
          foreach (Material mat in material) {
            mat.SetColor("_MainColor", Color.red);
            hitChanged = true;
          }
        }
      }

      if (ESM.SC.currentState != hitstunState) {
        if (hitChanged == true) {
          int index = 0;
          foreach (Material mat in material) {
            mat.SetColor("_MainColor", previousColor[index]);
            hitChanged = false;
            index += 1;
          }
        }
      }

    }

}
