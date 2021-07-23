using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Augments/VampireAugment ")]
public class VampireAugment : Item
{
    public override void Equip(playerStatManager pStatManager) {
        StatModifier mod = new StatModifier(2, StatModType.Flat, this);
        pStatManager.lifeSteal.AddModifier(mod);
    }
    public override void Unequip(playerStatManager pStatManager) {
        pStatManager.lifeSteal.RemoveAllModifiersFromSource(this);
    }
}
