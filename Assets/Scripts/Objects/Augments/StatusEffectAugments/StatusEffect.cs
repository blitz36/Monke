using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public abstract class StatusEffect : MonoBehaviour
{
  public float duration;
  public float timeBetweenTicks;
  public int stacks;
//  public bool isTimeReset;
  public EnemyStatManager ESM;
  public StatusEffectManager SEM;

  public IEnumerator DestroySelf(){
    yield return new WaitForSeconds(duration);
    SEM.statusEffects.Remove(this);
    Destroy(gameObject);
  }

  public abstract void tick();

}
