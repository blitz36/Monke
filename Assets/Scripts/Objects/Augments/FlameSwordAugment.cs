using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

[CreateAssetMenu(fileName = "New Item", menuName = "Modifiers/FlameSwordAugment")]
public class FlameSwordAugment : Item
{
    public override void Equip(playerStatManager pStatManager) {
        StatModifier mod = new StatModifier(3, StatModType.Flat, this);
      //  pStatManager.flameStat.AddModifier(mod);
    }
    public override void Unequip(playerStatManager pStatManager) {
        //pStatManager.flameStat.RemoveAllModifiersFromSource(this);
    }
}
