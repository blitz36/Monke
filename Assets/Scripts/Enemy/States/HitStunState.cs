using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStunState : State
{
  public float hitstunTime;
  public float hitstunTimer = 0f;
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
    hitstunTimer += Time.deltaTime;
    if (hitstunTimer > hitstunTime) {

      ESM.notHit();
      hitstunTimer = 0f;
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
