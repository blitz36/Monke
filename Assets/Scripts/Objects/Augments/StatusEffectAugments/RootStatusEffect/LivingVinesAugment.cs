using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/LivingVinesAugment ")]
public class LivingVinesAugment : Item
{
  public GameObject StatusEffect;
  public GameObject statusEffectManager;
  //if stat doesnt exist yet, then add it in
  //if it does, then just do the modifier
    public override void Equip(playerStatManager pStatManager) {
        if (!pStatManager.augmentHitboxes.ContainsKey("LivingVinesAugment")) {
          pStatManager.augmentHitboxes["LivingVinesAugment"] = StatusEffect;

            //add in the new delegate for each hitbox to use
          foreach (GameObject hitbox in pStatManager.lightAttackHitboxes) { //add in the new delegate for each hitbox to use
            HitboxController HC = hitbox.GetComponent<HitboxController>();
            HC.augmentedHitboxFunc += Root; //whenever hitbox triggers, do the code below
          }

        }

    }


    public override void Unequip(playerStatManager pStatManager) {
      pStatManager.augmentHitboxes.Remove("LivingVinesAugment");
      foreach (GameObject hitbox in pStatManager.lightAttackHitboxes) { //add in the new delegate for each hitbox to use
        HitboxController HC = hitbox.GetComponent<HitboxController>();
        HC.augmentedHitboxFunc -= Root; //whenever hitbox triggers, do the code below
      }

    }

    public void Root(EnemyStatManager ESM, playerStatManager PSM) {
      StatusEffectManager SEManager = ESM.gameObject.GetComponentInChildren<StatusEffectManager>();
      if (SEManager == null) {
        SEManager = Instantiate(statusEffectManager).GetComponent<StatusEffectManager>();
        SEManager.transform.parent = ESM.gameObject.transform;
      }
      if (SEManager.GetComponentInChildren<RootStatusEffect>() == null) { //only apply root if isnt applied

        RootStatusEffect RootStatus = Instantiate(PSM.augmentHitboxes["LivingVinesAugment"]).GetComponent<RootStatusEffect>();
        RootStatus.transform.parent = SEManager.transform;
        RootStatus.ESM = ESM;
        RootStatus.SEM = SEManager;
        SEManager.statusEffects.Add(RootStatus);
      }
    }
}
