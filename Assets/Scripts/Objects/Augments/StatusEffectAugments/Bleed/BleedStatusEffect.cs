using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedStatusEffect : StatusEffect
{

  public float damage;

  void Start(){
    StartCoroutine(DestroySelf());
  }

  public override void tick() {
    ESM.TakeDamage(damage);
  }

}
