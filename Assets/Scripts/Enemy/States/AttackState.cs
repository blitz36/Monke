using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
  public EnemyAttack attack;

  public override State runCurrentStateUpdate(StateController controller)
  {
    if (ESM.isHit == true) {
      return hitstunState;
    }
    base.runCurrentStateUpdate(controller);
    return this;
  }

  public override void runCurrentStateFixedUpdate(StateController controller)
  {

  }

  public override void runCurrentStateOnTriggerEnter(Collider other, StateController controller)
  {

  }

}
