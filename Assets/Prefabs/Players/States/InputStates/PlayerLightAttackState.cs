using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightAttackState : PlayerState
{
  public float timeBeforeBuffer; //amount of time before the attack ends when you can buffer a new attack
  private float bufferTimer = 0f;

  public List<Attack> lightAttack;
  public float ComboRecoveryTime;
  public float ComboBreakTime;
  public float ComboBreakTime2;
  private bool isComboContinued = false;

  private PlayerDashState dashState;
  private PlayerHeavyAttackState heavyAttackState;
  private PlayerBlockState blockState;
  private PlayerEquipState equipState;
  private PlayerMovementState movementState;

  void Start(){
    refreshEquips();
  }

  public override void Awake() {
    base.Awake();
    if (dashState == null) {
      dashState = gameObject.transform.root.GetComponentInChildren<PlayerDashState>();
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
    if (movementState == null) {
      movementState = gameObject.transform.root.GetComponentInChildren<PlayerMovementState>();
    }
  }


  public override PlayerState runCurrentStateUpdate(PlayerStateController controller)
  {
    if (PSM.playerInput.Base.Dashing.triggered && PSM.numDashes > 0) {
      lightAttack[PSM.comboStep].Cancel();
      bufferTimer = 0f;
      return dashState;
    }
    if (PSM.blockTrigger) {
      lightAttack[PSM.comboStep].Cancel();
      bufferTimer = 0f;
      return blockState;
    }

    if (PSM.playerInput.Base.Equip.triggered && equipState.canUseEquip) {
      lightAttack[PSM.comboStep].Cancel();
      bufferTimer = 0f;
      return equipState;
    }
    checkAttacks();
    return doAttack();
  }

  public override void runCurrentStateFixedUpdate(PlayerStateController controller)
  {

  }

  public override void runCurrentStateOnTriggerEnter(Collider other, PlayerStateController controller)
  {

  }

  public PlayerState doAttack() {
      isComboContinued = true;
      int State = 0;
      State = lightAttack[PSM.comboStep].PerformAttack(PSM);

      //if done with combo and at the end
      if (PSM.comboStep > lightAttack.Count - 2 && State == 0) {
        PSM.comboStep = -1;
        bufferTimer = 0f;
        StartCoroutine("ComboRecovery");
        isComboContinued = true;
        return movementState;
      }
      else if (PSM.comboStep > 0 && State == 0) {
        PSM.comboStep += 1;
        StartCoroutine("ComboBreak");
        isComboContinued = false;
        bufferTimer = 0f;
        return movementState;
      }
      else if (PSM.comboStep == 0 && State == 0) {
        PSM.comboStep += 1;
        StartCoroutine("ComboBreak");
        isComboContinued = false;
        return movementState;
      }
    //  else if(PSM.comboStep < 0) {
  //      return movementState;
//      }

      return this;
  }

  public void refreshEquips() {
    lightAttack = PSM.lightAttack;
  }

  IEnumerator ComboRecovery() {
    yield return new WaitForSeconds(ComboRecoveryTime);
    PSM.comboStep = 0;
  }

  IEnumerator ComboBreak() {
    if (PSM.comboStep == 1) {
      yield return new WaitForSeconds(ComboBreakTime);
    }
    else if (PSM.comboStep == 2) {
      yield return new WaitForSeconds(ComboBreakTime2);
    }
    if (isComboContinued == false) {
      PSM.comboStep = 0;
    }
  }

  void checkAttacks() {
    if (PSM.comboStep < 0) return;
    float timeToBuffer = lightAttack[PSM.comboStep].totalTime() - timeBeforeBuffer; //PROBLEM WHEN IT GOES TO -1 IN COMBO STEP
    bufferTimer += Time.deltaTime;
    if (bufferTimer < timeToBuffer) return;

    if (PSM.playerInput.Base.Attack.triggered) {
      PSM.bufferedAttack = true;
    }
    if (PSM.playerInput.Base.HeavyAttack.triggered) {
      PSM.bufferedAttack = true;
      PSM.chargeAttack = true;
    }
  }

}
