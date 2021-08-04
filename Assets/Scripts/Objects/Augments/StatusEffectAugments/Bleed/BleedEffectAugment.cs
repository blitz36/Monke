using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/BleedEffectAugment ")]
public class BleedEffectAugment : Item
{

  public GameObject StatusEffect;
  public GameObject statusEffectManager;
  //if stat doesnt exist yet, then add it in
  //if it does, then just do the modifier
    public override void Equip(playerStatManager pStatManager) {
        if (!pStatManager.augmentHitboxes.ContainsKey("BleedEffect")) {
          pStatManager.augmentHitboxes["BleedEffect"] = StatusEffect;

            //add in the new delegate for each hitbox to use
          foreach (GameObject hitbox in pStatManager.lightAttackHitboxes) { //add in the new delegate for each hitbox to use
            HitboxController HC = hitbox.GetComponent<HitboxController>();
            HC.augmentedHitboxFunc += Bleed; //whenever hitbox triggers, do the code below
          }

        }

        else {
//          StatModifier mod = new StatModifier(2, StatModType.Flat, this);//the modifier is add 2 lifesteal for every stack you get of this augment
  //        pStatManager.newStats["LifeSteal"].AddModifier(mod);
        }
    }


    public override void Unequip(playerStatManager pStatManager) {
      pStatManager.augmentHitboxes.Remove("BleedEffect");
      foreach (GameObject hitbox in pStatManager.lightAttackHitboxes) { //add in the new delegate for each hitbox to use
        HitboxController HC = hitbox.GetComponent<HitboxController>();
        HC.augmentedHitboxFunc -= Bleed; //whenever hitbox triggers, do the code below
      }

    }

    public void Bleed(EnemyStatManager ESM, playerStatManager PSM) {
      StatusEffectManager SEManager = ESM.gameObject.GetComponentInChildren<StatusEffectManager>();
      if (SEManager == null) {
        SEManager = Instantiate(statusEffectManager).GetComponent<StatusEffectManager>();
        SEManager.transform.parent = ESM.gameObject.transform;
      }
      BleedStatusEffect bleedStatus = Instantiate(PSM.augmentHitboxes["BleedEffect"]).GetComponent<BleedStatusEffect>();
      bleedStatus.transform.parent = SEManager.transform;
      bleedStatus.ESM = ESM;
      bleedStatus.SEM = SEManager;
      SEManager.statusEffects.Add(bleedStatus);
    }
}
