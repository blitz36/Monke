using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/FrostEffectAugment ")]
public class FrostAugment : Item
{
  public GameObject StatusEffect;
  public GameObject statusEffectManager;
  //if stat doesnt exist yet, then add it in
  //if it does, then just do the modifier
    public override void Equip(playerStatManager pStatManager) {
        if (!pStatManager.augmentHitboxes.ContainsKey("FrostAugment")) {
          pStatManager.augmentHitboxes["FrostAugment"] = StatusEffect;

            //add in the new delegate for each hitbox to use
          foreach (GameObject hitbox in pStatManager.lightAttackHitboxes) { //add in the new delegate for each hitbox to use
            HitboxController HC = hitbox.GetComponent<HitboxController>();
            HC.augmentedHitboxFunc += Slow; //whenever hitbox triggers, do the code below
          }

        }

        else {
//          StatModifier mod = new StatModifier(2, StatModType.Flat, this);//the modifier is add 2 lifesteal for every stack you get of this augment
  //        pStatManager.newStats["LifeSteal"].AddModifier(mod);
        }
    }


    public override void Unequip(playerStatManager pStatManager) {
      pStatManager.augmentHitboxes.Remove("FrostAugment");
      foreach (GameObject hitbox in pStatManager.lightAttackHitboxes) { //add in the new delegate for each hitbox to use
        HitboxController HC = hitbox.GetComponent<HitboxController>();
        HC.augmentedHitboxFunc -= Slow; //whenever hitbox triggers, do the code below
      }

    }

    public void Slow(EnemyStatManager ESM, playerStatManager PSM) {
      StatusEffectManager SEManager = ESM.gameObject.GetComponentInChildren<StatusEffectManager>();
      if (SEManager == null) {
        SEManager = Instantiate(statusEffectManager).GetComponent<StatusEffectManager>();
        SEManager.transform.parent = ESM.gameObject.transform;
      }
      if (SEManager.GetComponentInChildren<FrostStatusEffect>() == null) { //only apply frost if already slowed

        FrostStatusEffect slowStatus = Instantiate(PSM.augmentHitboxes["FrostAugment"]).GetComponent<FrostStatusEffect>();
        slowStatus.transform.parent = SEManager.transform;
        slowStatus.ESM = ESM;
        slowStatus.SEM = SEManager;
        SEManager.statusEffects.Add(slowStatus);
      }
    }
}
