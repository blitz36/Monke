using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
  public int state;
  public EnemyAttack attack;
  private DefaultState defaultState;
  private StunnedState stunnedState;
  public override void Awake() {
    base.Awake();
    if (defaultState == null) {
      defaultState = gameObject.transform.parent.GetComponentInChildren<DefaultState>();
    }
    if (stunnedState == null) {
      stunnedState = gameObject.transform.parent.GetComponentInChildren<StunnedState>();
    }
    attack.createHitbox(gameObject.transform);
  }


  public override State runCurrentStateUpdate(StateController controller)
  {
    if (ESM.isHit == true) {
      attack.Cancel();
      return hitstunState;
    }
    if (ESM.stunned) {
      attack.Cancel();
      return stunnedState;
    }

    state = attack.PerformAttack(ESM.rb, ESM.target);

    if (state == 0) {
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

}
