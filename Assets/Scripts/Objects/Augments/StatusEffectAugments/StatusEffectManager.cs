using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    public float timeBetweenTicks;

    void Start() {
      StartCoroutine(tickCycle(timeBetweenTicks));
    }

    public IEnumerator tickCycle(float timeBetweenTicks) {
      while (true) {
        foreach (StatusEffect effect in statusEffects) {
          effect.tick();
        }
        yield return new WaitForSeconds(timeBetweenTicks);
      }
    }

}
