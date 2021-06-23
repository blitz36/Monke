using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public abstract class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public string description;
    public abstract void Equip(playerStatManager pStatManager);
    public abstract void Unequip(playerStatManager pStatManager);

}
