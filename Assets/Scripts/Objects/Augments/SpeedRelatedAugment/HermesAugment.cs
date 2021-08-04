using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/HermesAugment ")]
public class HermesAugment : Item
{
    public override void Equip(playerStatManager pStatManager) {
        StatModifier mod = new StatModifier(.1f, StatModType.PercentAdd, this);
        pStatManager.baseSpeed.AddModifier(mod);

        GameObject leftBoot = Instantiate(physicalItem[0]);
    //    leftBoot.transform.rotation = Quaternion.identity;
        leftBoot.transform.parent = pStatManager.transform.FindDeepChild("LeftFoot_jnt");
        leftBoot.transform.rotation = Quaternion.identity;
        leftBoot.transform.localPosition = new Vector3(0f,0f,0f);
        leftBoot.transform.localScale = new Vector3(1f,1f,1f);

        GameObject RightBoot = Instantiate(physicalItem[1]);

        RightBoot.transform.parent = pStatManager.transform.FindDeepChild("RightFoot_jnt");
        RightBoot.transform.rotation = Quaternion.identity;
        RightBoot.transform.localPosition = new Vector3(0f,0f,0f);
        RightBoot.transform.localScale = new Vector3(1f,1f,1f);
    }
    public override void Unequip(playerStatManager pStatManager) {
        pStatManager.baseSpeed.RemoveAllModifiersFromSource(this);
        Transform leftBoot = pStatManager.transform.FindDeepChild("Hermes_L_Augment");
        Transform rightBoot = pStatManager.transform.FindDeepChild("Hermes_R_Augment");
        Destroy(leftBoot);
        Destroy(rightBoot);
    }
}
