using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public class FrostStatusEffect : StatusEffect
{
  public float damage;
  public float slowdownMultiplier;
  void Start(){
    StartCoroutine(DestroySelf());
    StatModifier mod = new StatModifier(slowdownMultiplier, StatModType.PercentMult, this);
    ESM.baseSpeed.AddModifier(mod);
  }

  public override void tick() {
    ESM.TakeDamage(damage);
  }

  void OnDestroy() {
    ESM.baseSpeed.RemoveAllModifiersFromSource(this);
  }
}
