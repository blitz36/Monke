using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
  public float timeToDissolve;
  private float timerToDissolve;

  public override State runCurrentStateUpdate(StateController controller)
  {
    base.runCurrentStateUpdate(controller);
    ESM.rb.velocity = new Vector3(0f,0f,0f);
    timerToDissolve += Time.deltaTime;
    if (timerToDissolve > timeToDissolve) {
      if (ESM.EDT != null) {
        ESM.EDT.DecideDrop();
      }
      ESM.destroySelf();
    }

    if (ESM.healthBar){
      Destroy(ESM.healthBar.gameObject);
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
