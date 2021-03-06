using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Modifiers/SpdItem")]
public class SpdItem : Item
{
    public override void Equip(playerStatManager pStatManager) {
        StatModifier mod = new StatModifier(2, StatModType.Flat, this);
        pStatManager.baseSpeed.AddModifier(mod);
    }
    public override void Unequip(playerStatManager pStatManager) {
        pStatManager.baseSpeed.RemoveAllModifiersFromSource(this);
    }
}
