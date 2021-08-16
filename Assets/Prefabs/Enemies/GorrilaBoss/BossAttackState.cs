using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : CombatState
{
  public int state;
  public EnemyAttack attack;
  public EnemyAttack flameThrower;
  public EnemyAttack Laser;
  private DefaultState defaultState;
  private StunnedState stunnedState;
  private bool inMeleeAttack;
  public bool isInMeleeRange;
  public float meleeRange;
  [SerializeField] private LayerMask playerLayer;

  public override void Awake() {
    base.Awake();
    if (defaultState == null) {
      defaultState = gameObject.transform.parent.GetComponentInChildren<DefaultState>();
    }
    if (stunnedState == null) {
      stunnedState = gameObject.transform.parent.GetComponentInChildren<StunnedState>();
    }
    attack.createHitbox(gameObject.transform);
    Laser.createHitbox(gameObject.transform);
    flameThrower.createHitbox(gameObject.transform);
  }


  public override State runCurrentStateUpdate(StateController controller)
  {
    ESM.currentAnim = 2;
    if (ESM.isHit == true) {
      attack.Cancel();
      return hitstunState;
    }
    if (ESM.stunned) {
      attack.Cancel();
      return stunnedState;
    }
    if (state == 0) {
      isInMeleeRange = Physics.CheckSphere(transform.position, meleeRange, playerLayer);
    }

    if (isInMeleeRange) {
      state = attack.PerformAttack(ESM.rb, ESM.target);
    }
    else {
      state = flameThrower.PerformAttack(ESM.rb, ESM.target);
    }
    if (ESM.currentHealth < ESM.maxHealth.Value/2) {
      Laser.PerformAttack(ESM.rb, ESM.target);
    }

    if (state == 0) {
      Laser.Cancel();
      return defaultState;
    }

    return this;
  }

  public override void runCurrentStateFixedUpdate(StateController controller)
  {
  }

  public override void runCurrentStateOnTriggerEnter(Collider other, StateController controller)
  {

  }

  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
    Gizmos.DrawWireSphere (transform.position, meleeRange);
  }

}
