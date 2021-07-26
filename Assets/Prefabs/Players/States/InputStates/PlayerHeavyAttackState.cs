using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyAttackState : PlayerState
{
  private PlayerDashState dashState;
  private PlayerBlockState blockState;
  private PlayerEquipState equipState;
  private PlayerMovementState movementState;
  private int filler;
  public Attack heavyAttack;

  void Start() {
    refreshEquips();
  }

  public override void Awake() {
    base.Awake();
    if (dashState == null) {
      dashState = gameObject.transform.root.GetComponentInChildren<PlayerDashState>();
    }

    if (blockState == null) {
      blockState = gameObject.transform.root.GetComponentInChildren<PlayerBlockState>();
    }

    if (equipState == null) {
      equipState = gameObject.transform.root.GetComponentInChildren<PlayerEquipState>();
    }
    if (movementState == null) {
      movementState = gameObject.transform.root.GetComponentInChildren<PlayerMovementState>();
    }
  }

  public override PlayerState runCurrentStateUpdate(PlayerStateController controller)
  {
    if (PSM.playerInput.Base.Dashing.triggered && PSM.numDashes > 0) {
      heavyAttack.Cancel();
      PSM.chargeAttack = false;
      return dashState;
    }
    if (PSM.playerInput.Base.Block.triggered) {
      heavyAttack.Cancel();
      PSM.chargeAttack = false;
      return blockState;
    }

    if (PSM.playerInput.Base.Equip.triggered && equipState.canUseEquip) {
      heavyAttack.Cancel();
      PSM.chargeAttack = false;
      return equipState;
    }

    int State = heavyAttack.PerformAttack(PSM);
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
    heavyAttack = PSM.heavyAttack;
  }
}
