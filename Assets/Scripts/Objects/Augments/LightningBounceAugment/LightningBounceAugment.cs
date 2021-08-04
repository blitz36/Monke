using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/LightningBounceAugment")]
public class LightningBounceAugment : Item
{
  public GameObject hitbox;
  //if stat doesnt exist yet, then add it in
  //if it does, then just do the modifier
    public override void Equip(playerStatManager pStatManager) {
        if (!pStatManager.augmentHitboxes.ContainsKey("LightningBounceHitbox")) {
          pStatManager.augmentHitboxes["LightningBounceHitbox"] = hitbox;
          GameObject lightning = pStatManager.augmentHitboxes["LightningBounceHitbox"]; // assign the prefab to dict

          if (!pStatManager.newStats.ContainsKey("numBounces")) {
            pStatManager.newStats["numBounces"] = new CharacterStat(0f); //put the new stat in the dict
          }

            //add in the new delegate for each hitbox to use
          foreach (GameObject hitbox in pStatManager.hitboxes) {
            HitboxController HC = hitbox.GetComponent<HitboxController>();
            HC.augmentedHitboxFunc += LightningStrike;
          }

        }

        StatModifier mod = new StatModifier(2, StatModType.Flat, this);//the modifier is add 2 lifesteal for every stack you get of this augment
        pStatManager.newStats["numBounces"].AddModifier(mod);
    }


    public override void Unequip(playerStatManager pStatManager) {
        pStatManager.newStats["numBounces"].RemoveAllModifiersFromSource(this);
        pStatManager.augmentHitboxes.Remove("LightningBounceHitbox");
        foreach (GameObject hitbox in pStatManager.lightAttackHitboxes) { //add in the new delegate for each hitbox to use
          HitboxController HC = hitbox.GetComponent<HitboxController>();
          HC.augmentedHitboxFunc -= LightningStrike; //whenever hitbox triggers, do the code below
        }

    }

    public void LightningStrike(EnemyStatManager ESM, playerStatManager PSM) {

      GameObject lightning = Instantiate(PSM.augmentHitboxes["LightningBounceHitbox"]);
      lightning.transform.position = PSM.gameObject.transform.position;

      LightningHitbox hitbox = lightning.GetComponent<LightningHitbox>();
      hitbox.numBounces = (int)PSM.newStats["numBounces"].Value;
      hitbox.target = ESM.transform;


    }
}
