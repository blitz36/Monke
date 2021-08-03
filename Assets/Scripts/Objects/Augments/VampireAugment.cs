using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/VampireAugment ")]
public class VampireAugment : Item
{
  //if stat doesnt exist yet, then add it in
  //if it does, then just do the modifier
    public override void Equip(playerStatManager pStatManager) {
        if (!pStatManager.newStats.ContainsKey("LifeSteal")) {
          pStatManager.newStats["LifeSteal"] = new CharacterStat(0f); //put the new stat in the dict
          StatModifier mod = new StatModifier(2, StatModType.Flat, this);//the modifier is add 2 lifesteal for every stack you get of this augment
          pStatManager.newStats["LifeSteal"].AddModifier(mod);

          foreach (GameObject hitbox in pStatManager.hitboxes) { //add in the new delegate for each hitbox to use
            HitboxController HC = hitbox.GetComponent<HitboxController>();
            HC.augmentedHitboxFunc += LifeSteal; //whenever hitbox triggers, do the lifesteal code below
          }
        }

        else {
          StatModifier mod = new StatModifier(2, StatModType.Flat, this);//the modifier is add 2 lifesteal for every stack you get of this augment
          pStatManager.newStats["LifeSteal"].AddModifier(mod);
        }
    }


    public override void Unequip(playerStatManager pStatManager) {
        pStatManager.newStats["LifeSteal"].RemoveAllModifiersFromSource(this);
        foreach (GameObject hitbox in pStatManager.hitboxes) {
          HitboxController HC = hitbox.GetComponent<HitboxController>();
          HC.augmentedHitboxFunc -= LifeSteal;
        }
    }

    public void LifeSteal(EnemyStatManager ESM, playerStatManager PSM) {
      float lifeSteal = PSM.newStats["LifeSteal"].Value; //get lifesteal value out of dict
      if (PSM.currentHealth <= PSM.maxHealth.Value) { //if not max health yet
        PSM.currentHealth += lifeSteal; //life steal
        PSM.healthBar.SetHealth(PSM.currentHealth/PSM.maxHealth.Value); //update healthbar
      }
    }
}
