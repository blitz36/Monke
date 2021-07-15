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
    }
    public override void Unequip(playerStatManager pStatManager) {
        pStatManager.baseDamage.RemoveAllModifiersFromSource(this);
        pStatManager.updateDmgValues();
    }
}
