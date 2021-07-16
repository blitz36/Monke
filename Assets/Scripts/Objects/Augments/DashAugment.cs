using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/DashAugment ")]
public class DashAugment : Item
{
    public override void Equip(playerStatManager pStatManager) {
        StatModifier mod = new StatModifier(1, StatModType.Flat, this);
        pStatManager.maxDashes.AddModifier(mod);
    }
    public override void Unequip(playerStatManager pStatManager) {
        pStatManager.maxDashes.RemoveAllModifiersFromSource(this);
    }
}
