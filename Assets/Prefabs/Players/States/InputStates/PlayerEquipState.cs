using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipState : PlayerState
{
  public Equipable equip;
  public bool canUseEquip = true;
  public float equipCDTime;

  private PlayerDashState dashState;
  private PlayerMovementState movementState;
  public override void Awake() {
    base.Awake();
    if (dashState == null) {
      dashState = gameObject.transform.root.GetComponentInChildren<PlayerDashState>();
    }

    if (movementState == null) {
      movementState = gameObject.transform.root.GetComponentInChildren<PlayerMovementState>();
    }
  }

  void Start(){
    refreshEquips();
    equipCDTime = equip.Cooldown;
  }

  public override PlayerState runCurrentStateUpdate(PlayerStateController controller)
  {
    if (PSM.playerInput.Base.Dashing.triggered && PSM.numDashes > 0) {
      equip.Cancel();
      return dashState;
    }

      if (equip.State == 0) {
        canUseEquip = false;
        Invoke("equipOffCD", equipCDTime);
      }

      int State = equip.Activate(PSM);
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
    equip = PSM.equip;
  }

  public void equipOffCD() {
    canUseEquip = true;
  }
}
