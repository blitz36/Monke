using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public abstract class Item : ScriptableObject
{
    public string name = "New Item";
    public Sprite icon = null;
    public string Description;
    public abstract void Equip(playerStatManager pStatManager);
    public abstract void Unequip(playerStatManager pStatManager);

}
