using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
  new public string name = "New Attack";
  public Sprite icon = null;
  public string description;
  public int damage;
  public List<GameObject> hitboxes;

  public abstract float totalTime();
  public abstract List<GameObject> createHitbox(Transform Player);
  public abstract int PerformAttack(playerStatManager PSM);
  public abstract void Cancel(playerStatManager PSM);
}
