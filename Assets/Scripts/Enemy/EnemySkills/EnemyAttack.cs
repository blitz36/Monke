using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : ScriptableObject
{
  new public string name = "New Attack";
  public Sprite icon = null;
  public string description;
  public int damage;
  public List<GameObject> hitboxes;

  public abstract void createHitbox(Transform target);
  public abstract void PerformAttack(ref bool isAttack, Rigidbody rb, ref float cooldownTimer, float cooldownTime, Transform target);
  public abstract void Cancel();
}
