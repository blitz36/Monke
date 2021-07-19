using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour {
  public Sprite icon = null;
  public string description;
  public int damage;
  public List<GameObject> hitboxes;

  public abstract void createHitbox(Transform target);
  public abstract int PerformAttack(Rigidbody rb, Transform target);
  public abstract void Cancel();
}
