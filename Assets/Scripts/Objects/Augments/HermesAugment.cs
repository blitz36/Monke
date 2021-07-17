using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/HermesAugment ")]
public class HermesAugment : Item
{
    public override void Equip(playerStatManager pStatManager) {
        StatModifier mod = new StatModifier(1, StatModType.PercentAdd, this);
        pStatManager.baseSpeed.AddModifier(mod);
    }
    public override void Unequip(playerStatManager pStatManager) {
        pStatManager.baseSpeed.RemoveAllModifiersFromSource(this);
    }
}
