using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/HeavyArmamentsAugment")]
public class HeavyArmamentsAugment : Item
{
  public GameObject hitbox;
  public LayerMask layerMask;
  public float missileRange;
  //if stat doesnt exist yet, then add it in
  //if it does, then just do the modifier
    public override void Equip(playerStatManager pStatManager) {
        if (!pStatManager.augmentHitboxes.ContainsKey("HeavyArmaments")) {
          pStatManager.augmentHitboxes["HeavyArmaments"] = hitbox;
          GameObject missile = pStatManager.augmentHitboxes["HeavyArmaments"]; // assign the prefab to dict

          if (!pStatManager.newStats.ContainsKey("ArmamentsNumMissiles")) {
            pStatManager.newStats["ArmamentsNumMissiles"] = new CharacterStat(0f); //put the new stat in the dict
          }

            //add in the new delegate for each hitbox to use
          foreach (GameObject hitbox in pStatManager.hitboxes) {
            HitboxController HC = hitbox.GetComponent<HitboxController>();
            HC.augmentedHitboxFunc += FireMissiles;
          }
          GameObject HeavyArmaments = Instantiate(physicalItem[0]);
      //    leftBoot.transform.rotation = Quaternion.identity;
          HeavyArmaments.transform.parent = pStatManager.transform.FindDeepChild("Spine3_jnt");
          HeavyArmaments.transform.localRotation = Quaternion.identity;
          HeavyArmaments.transform.localPosition = new Vector3(0f,0f,0f);
          HeavyArmaments.transform.localScale = new Vector3(1f,1f,1f);

        }

        StatModifier mod = new StatModifier(1, StatModType.Flat, this);//the modifier is add 2 lifesteal for every stack you get of this augment
        pStatManager.newStats["ArmamentsNumMissiles"].AddModifier(mod);
    }


    public override void Unequip(playerStatManager pStatManager) {
        pStatManager.newStats["ArmamentsNumMissiles"].RemoveAllModifiersFromSource(this);
        pStatManager.augmentHitboxes.Remove("HeavyArmaments");
        foreach (GameObject hitbox in pStatManager.lightAttackHitboxes) { //add in the new delegate for each hitbox to use
          HitboxController HC = hitbox.GetComponent<HitboxController>();
          HC.augmentedHitboxFunc -= FireMissiles; //whenever hitbox triggers, do the code below
        }
        Transform HeavyArmaments = pStatManager.transform.FindDeepChild("HeavyArmaments_Augment");
        Destroy(HeavyArmaments);

    }

    public void FireMissiles(EnemyStatManager ESM, playerStatManager PSM) {
      //do 20% chance here eventually
      Collider[] hitColliders = Physics.OverlapSphere(PSM.transform.position, missileRange, layerMask);

      if (hitColliders.Length < 1) {return;}
      for (int i = 0; i < (int) PSM.newStats["ArmamentsNumMissiles"].Value; i++) {
        Collider target = hitColliders[Random.Range(0, hitColliders.Length)];
        GameObject missile = Instantiate(PSM.augmentHitboxes["HeavyArmaments"]);
        missile.transform.position = PSM.gameObject.transform.position;

        MissilesHitboxController hitbox = missile.GetComponent<MissilesHitboxController>();
        hitbox.target = target.transform;
      }

    }
}
