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

  public abstract void createHitbox(Transform Player);
  public abstract void PerformAttack(Rigidbody rb, Plane plane, GameObject gameObject, ref bool bufferAttack, ref int priority, ref int comboStep, int nextStep);
  public abstract void Cancel();
}
