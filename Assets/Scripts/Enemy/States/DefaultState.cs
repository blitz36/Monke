using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : State
{
  private MovementState movementState;
  private StunnedState stunnedState;
  public float triggerRange;
  [SerializeField] private LayerMask playerLayer;
  private bool isInTriggerRange = false;

  public override void Awake(){
    base.Awake();
    if (movementState == null) {
      movementState = gameObject.transform.parent.GetComponentInChildren<MovementState>();
    }
    if (stunnedState == null) {
      stunnedState = gameObject.transform.parent.GetComponentInChildren<StunnedState>();
    }
  }

  public override State runCurrentStateUpdate(StateController controller)
  {
    if (ESM.isHit == true) {
      return hitstunState;
    }

    if (ESM.stunned) {
      return stunnedState;
    }

    base.runCurrentStateUpdate(controller);
    if (isInTriggerRange) {
      return movementState;
    }
    return this;
  }

  public override void runCurrentStateFixedUpdate(StateController controller)
  {
    isInTriggerRange = Physics.CheckSphere(transform.position, triggerRange, playerLayer);
  }

  public override void runCurrentStateOnTriggerEnter(Collider other, StateController controller)
  {

  }

  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
    Gizmos.DrawWireSphere (transform.position, triggerRange);
  }

}
