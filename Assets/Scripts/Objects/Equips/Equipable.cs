using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipable : ScriptableObject
{
  new public string name = "New Item";
  public Sprite icon = null;
  public string description;
  public int damage;
  public List<GameObject> hitboxes;

  public abstract void Cancel();
  public abstract List<GameObject> createHitbox(Transform Player);
  public abstract void Activate(Rigidbody rb, Plane plane, GameObject gameObject, ref int priority);
}
