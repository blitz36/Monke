using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStatusEffect : StatusEffect
{

  public float damage;
  private int counter = 0;
  public GameObject fireHitbox;
  public float explosionRange;

  void Start(){
    StartCoroutine(DestroySelf());
  }

  public override void tick() {
    if (counter == 3) {
      fireHitbox.SetActive(true);
      counter = 0;
    }
    else {
      ESM.TakeDamage(damage);
      counter += 1;
    }

  }

}
