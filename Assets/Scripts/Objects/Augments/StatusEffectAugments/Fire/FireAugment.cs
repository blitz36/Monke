using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/FireAugment ")]
public class FireAugment : Item
{

  public GameObject StatusEffect;
  public GameObject statusEffectManager;
  //if stat doesnt exist yet, then add it in
  //if it does, then just do the modifier
    public override void Equip(playerStatManager pStatManager) {
        if (!pStatManager.augmentHitboxes.ContainsKey("FireEffect")) {
          pStatManager.augmentHitboxes["FireEffect"] = StatusEffect;

            //add in the new delegate for each hitbox to use
          foreach (GameObject hitbox in pStatManager.lightAttackHitboxes) { //add in the new delegate for each hitbox to use
            HitboxController HC = hitbox.GetComponent<HitboxController>();
            HC.augmentedHitboxFunc += Flame; //whenever hitbox triggers, do the code below
          }

          if (!pStatManager.newStats.ContainsKey("FireRadius")) {
            pStatManager.newStats["FireRadius"] = new CharacterStat(0f); //put the new stat in the dict
        }
      }
        StatModifier mod = new StatModifier(4, StatModType.Flat, this);//the modifier is add 2 lifesteal for every stack you get of this augment
        pStatManager.newStats["FireRadius"].AddModifier(mod);

    }


    public override void Unequip(playerStatManager pStatManager) {
      pStatManager.augmentHitboxes.Remove("FireEffect");
      pStatManager.newStats.Remove("FireRadius");
      foreach (GameObject hitbox in pStatManager.lightAttackHitboxes) { //add in the new delegate for each hitbox to use
        HitboxController HC = hitbox.GetComponent<HitboxController>();
        HC.augmentedHitboxFunc -= Flame; //whenever hitbox triggers, do the code below
      }

    }

    public void Flame(EnemyStatManager ESM, playerStatManager PSM) {
      StatusEffectManager SEManager = ESM.gameObject.GetComponentInChildren<StatusEffectManager>();
      if (SEManager == null) {
        SEManager = Instantiate(statusEffectManager, ESM.gameObject.transform).GetComponent<StatusEffectManager>();
      }
      if (SEManager.GetComponentInChildren<FireStatusEffect>() == null) {
        FireStatusEffect fireStatus = Instantiate(PSM.augmentHitboxes["FireEffect"], SEManager.transform).GetComponent<FireStatusEffect>();
        fireStatus.ESM = ESM;
        fireStatus.SEM = SEManager;
        float size = PSM.newStats["FireRadius"].Value;
        fireStatus.fireHitbox.transform.localScale = new Vector3(size, fireStatus.fireHitbox.transform.localScale.y, size);
        SEManager.statusEffects.Add(fireStatus);
      }
    }
}
