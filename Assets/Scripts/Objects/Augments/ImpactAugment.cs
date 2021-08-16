using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/ImpactAugment")]
public class ImpactAugment : Item
{
    public override void Equip(playerStatManager pStatManager) {
        StatModifier mod = new StatModifier(10, StatModType.Flat, this);
        pStatManager.baseDamage.AddModifier(mod);
        pStatManager.updateDmgValues();

        GameObject equip = Instantiate(physicalItem[0]);
    //    leftBoot.transform.rotation = Quaternion.identity;
        equip.transform.parent = pStatManager.transform.FindDeepChild("LeftForeArm_jnt");
        equip.transform.localRotation = Quaternion.identity;
        equip.transform.localPosition = new Vector3(0f,0f,0f);
        equip.transform.localScale = new Vector3(1f,1f,1f);
    }
    public override void Unequip(playerStatManager pStatManager) {
        pStatManager.baseDamage.RemoveAllModifiersFromSource(this);
        pStatManager.updateDmgValues();

        Transform equip = pStatManager.transform.FindDeepChild("Impact_Augment");
        Destroy(equip);
    }
}
