using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrail : TrailType
{
  public GameObject statusEffectManager;
  public GameObject slowStatusPrefab;

  void OnTriggerEnter(Collider other) {
    EnemyStatManager ESM = other.gameObject.GetComponent<EnemyStatManager>();
    if (ESM == null) {return;}
    StatusEffectManager SEManager = ESM.gameObject.GetComponentInChildren<StatusEffectManager>();
    if (SEManager == null) {
      SEManager = Instantiate(statusEffectManager).GetComponent<StatusEffectManager>();
      SEManager.transform.parent = ESM.gameObject.transform;
    }

    if (SEManager.GetComponentInChildren<FrostStatusEffect>() == null) { //only apply frost if already slowed
      FrostStatusEffect slowStatus = Instantiate(slowStatusPrefab, SEManager.transform).GetComponent<FrostStatusEffect>();
      slowStatus.ESM = ESM;
      slowStatus.SEM = SEManager;
      SEManager.statusEffects.Add(slowStatus);
    }

  }
}
