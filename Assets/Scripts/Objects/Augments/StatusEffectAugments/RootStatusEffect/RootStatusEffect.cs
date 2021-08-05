using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public class RootStatusEffect : StatusEffect
{
  public float damage;
  public int ticksToActivate;
  public int ticksToDestroy;
  public int ticksToDestroyCounter = 0;
  public int tickCounter = 0;

  public float previousPercentHealth;
  public float diffToTick;
  private bool hasActivated = false;

  void Awake(){
    previousPercentHealth = ESM.currentHealth/ESM.maxHealth.Value;
  }

  public override void tick() {
    handleTickReduction();
    if (tickCounter >= ticksToActivate) {
      if (!hasActivated) {
        ESM.TakeDamage(damage);
        StatModifier mod = new StatModifier(-1, StatModType.PercentMult, this);
        ESM.baseSpeed.AddModifier(mod);
        hasActivated = true;
      }

      if (ticksToDestroyCounter == ticksToDestroy) {
        Destroy(gameObject);
      }
      ticksToDestroyCounter += 1; //dont tick to destroy until activated
    }
    tickCounter += 1; //tick to get some activation going
  }

  void OnDestroy() {
    ESM.baseSpeed.RemoveAllModifiersFromSource(this);
  }

  void handleTickReduction() {
    if (previousPercentHealth == ESM.currentHealth/ESM.maxHealth.Value) return;
    if (previousPercentHealth < ESM.currentHealth/ESM.maxHealth.Value) { //if they healed since the last tick
      previousPercentHealth = ESM.currentHealth/ESM.maxHealth.Value;
      return;
    }
    diffToTick += previousPercentHealth - ESM.currentHealth/ESM.maxHealth.Value;
    if (diffToTick > 0.05f) {
      tickCounter += (int)(diffToTick/0.05f);
      diffToTick -= Mathf.Round(diffToTick/0.05f);
    }

    previousPercentHealth = ESM.currentHealth/ESM.maxHealth.Value;
  }
}
