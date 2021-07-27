using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerState
{

  private PlayerDashState dashState;
  private PlayerLightAttackState lightAttackState;
  private PlayerHeavyAttackState heavyAttackState;
  private PlayerBlockState blockState;
  private PlayerEquipState equipState;

  public override void Awake() {
    base.Awake();
    if (dashState == null) {
      dashState = gameObject.transform.root.GetComponentInChildren<PlayerDashState>();
    }

    if (lightAttackState == null) {
      lightAttackState = gameObject.transform.root.GetComponentInChildren<PlayerLightAttackState>();
    }

    if (heavyAttackState == null) {
      heavyAttackState = gameObject.transform.root.GetComponentInChildren<PlayerHeavyAttackState>();
    }

    if (blockState == null) {
      blockState = gameObject.transform.root.GetComponentInChildren<PlayerBlockState>();
    }

    if (equipState == null) {
      equipState = gameObject.transform.root.GetComponentInChildren<PlayerEquipState>();
    }
  }


  public override PlayerState runCurrentStateUpdate(PlayerStateController controller)
  {
    Vector2 direction = PSM.playerInput.Base.Move.ReadValue<Vector2>();
    performMovement(direction.x, direction.y);
    checkAttacks();

    if (PSM.bufferedAttack && PSM.chargeAttack) {
      PSM.isRunning = false;
      return heavyAttackState;
    }

    if (PSM.bufferedAttack && PSM.comboStep >= 0) {
      PSM.isRunning = false;
      return lightAttackState;
    }

    if (PSM.blockTrigger) {
      PSM.holdTimer = 0f;
      PSM.isRunning = false;
      return blockState;
    }

    if (PSM.playerInput.Base.Equip.triggered && equipState.canUseEquip) {
      PSM.holdTimer = 0f;
      PSM.isRunning = false;
      return equipState;
    }

    if (PSM.playerInput.Base.Dashing.triggered && PSM.numDashes > 0) {
      PSM.holdTimer = 0f;
      PSM.isRunning = false;
      return dashState;
    }

    PSM.comboStep = 0;
    return this;
  }

  public override void runCurrentStateFixedUpdate(PlayerStateController controller)
  {

  }

  public override void runCurrentStateOnTriggerEnter(Collider other, PlayerStateController controller)
  {

  }

  //You can only move while not dashing or isnt fighting isAction checks for fighting
  //normal running stuff
  void performMovement(float horiz, float vert) {
        //if there is any direction inputs, run in that direction
        if (horiz != 0 || vert != 0){
          Vector3 fVelocity = new Vector3(horiz, 0, vert);
          Vector3 speed;
          if (PSM.holdTimer > 0) {
            speed = fVelocity.normalized * PSM.baseSpeed.Value * Mathf.Max((1-(PSM.holdTimer/3)), 0);
          }
          else {
            speed = fVelocity.normalized * PSM.baseSpeed.Value;
          }
          speed.y = PSM.rb.velocity.y;
          PSM.rb.velocity = speed;
          PSM.isRunning = true;
          transform.root.GetChild(0).rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(fVelocity.normalized), 0.2f);
        }

        else {
          PSM.rb.velocity = new Vector3(0,PSM.rb.velocity.y,0); //no input = stand still
          PSM.isRunning = false;
        }
  }

  void checkAttacks() {

    if (PSM.playerInput.Base.Attack.triggered && PSM.comboStep >= 0) {
      PSM.bufferedAttack = true;
    }
    if (PSM.playerInput.Base.HeavyAttack.triggered) {
      PSM.bufferedAttack = true;
      PSM.chargeAttack = true;
    }
  }

}
