using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/LaserSlashAugment")]
public class LaserSlashAugment : Item
{
  public GameObject hitbox;

  //if stat doesnt exist yet, then add it in
  //if it does, then just do the modifier
    public override void Equip(playerStatManager pStatManager) {
        if (!pStatManager.augmentHitboxes.ContainsKey("LaserSlash")) {
          pStatManager.augmentHitboxes["LaserSlash"] = Instantiate(hitbox);
          GameObject laser = pStatManager.augmentHitboxes["LaserSlash"];
          laser.transform.parent = pStatManager.gameObject.transform.parent;
          laser.transform.localPosition = new Vector3(0,0,0);
          laser.SetActive(false);

            //add in the new delegate for each hitbox to use
          HitboxController HC = pStatManager.lightAttackHitboxes[2].GetComponent<HitboxController>();
          HC.augmentedHitboxFunc += LaserSlash; //whenever hitbox triggers, do the lifesteal code below

        }

        else {
//          StatModifier mod = new StatModifier(2, StatModType.Flat, this);//the modifier is add 2 lifesteal for every stack you get of this augment
  //        pStatManager.newStats["LifeSteal"].AddModifier(mod);
        }
    }


    public override void Unequip(playerStatManager pStatManager) {
    //    pStatManager.newStats["LifeSteal"].RemoveAllModifiersFromSource(this);
        pStatManager.augmentHitboxes.Remove("LaserSlash");
        Destroy(pStatManager.augmentHitboxes["LaserSlash"]);
        HitboxController HC = pStatManager.lightAttackHitboxes[2].GetComponent<HitboxController>();
        HC.augmentedHitboxFunc -= LaserSlash; //whenever hitbox triggers, do the lifesteal code below

    }

    public void LaserSlash(EnemyStatManager ESM, playerStatManager PSM) {
      GameObject laser = PSM.augmentHitboxes["LaserSlash"];
      laser.transform.position = PSM.gameObject.transform.position;
      laser.transform.rotation = PSM.gameObject.transform.rotation;
      laser.SetActive(true);

    }
}
