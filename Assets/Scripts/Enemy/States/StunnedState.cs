using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : State
{
  public float stunTimer = 0f;
  private DefaultState defaultState;
  private MovementState movementState;

  public override void Awake() {
    base.Awake();
    if (defaultState == null) {
      defaultState = gameObject.transform.parent.GetComponentInChildren<DefaultState>();
    }
    if (movementState == null) {
      movementState = gameObject.transform.parent.GetComponentInChildren<MovementState>();
    }
  }

  public override State runCurrentStateUpdate(StateController controller)
  {
    stunTimer += Time.deltaTime;
    if (stunTimer > ESM.stunTime) {
      stunTimer = 0f;
      ESM.stunned = false;
      return defaultState;
    }

    if (ESM.currentHealth <= 0) {
      return deadState;
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
