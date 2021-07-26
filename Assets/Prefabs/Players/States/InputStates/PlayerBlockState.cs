using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerState
{
  public Block block;
  private PlayerDashState dashState;
  private PlayerMovementState movementState;
  private int filler;

  void Start() {
    refreshEquips();
  }

  public override void Awake() {
    base.Awake();
    if (dashState == null) {
      dashState = gameObject.transform.root.GetComponentInChildren<PlayerDashState>();
    }

    if (movementState == null) {
      movementState = gameObject.transform.root.GetComponentInChildren<PlayerMovementState>();
    }
  }

  public override PlayerState runCurrentStateUpdate(PlayerStateController controller)
  {
    if (PSM.playerInput.Base.Dashing.triggered && PSM.numDashes > 0) {
      block.Cancel();
      return dashState;
    }

    int State = block.PerformAttack(PSM);

    if (State == 0) {
      return movementState;
    }
    return this;
  }

  public override void runCurrentStateFixedUpdate(PlayerStateController controller)
  {

  }

  public override void runCurrentStateOnTriggerEnter(Collider other, PlayerStateController controller)
  {

  }

  public void refreshEquips() {
    block = PSM.block;
  }
}
