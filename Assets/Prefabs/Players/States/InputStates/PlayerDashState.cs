using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
  private PlayerMovementState MovementState;

  public float dashMultiplier = 5;
  public static int dashState = 0;
  public float dashTimer;
  public float maxDash;

    // Start is called before the first frame update
    public override void Awake()
    {
      base.Awake();
      if (MovementState == null) {
        MovementState = gameObject.transform.root.GetComponentInChildren<PlayerMovementState>();
      }
    }

    public override PlayerState runCurrentStateUpdate(PlayerStateController controller)
    {
      Vector2 direction = PSM.playerInput.Base.Move.ReadValue<Vector2>();
      PlayerState state = performDash(direction.x, direction.y);
      return state;
    }

    public override void runCurrentStateFixedUpdate(PlayerStateController controller)
    {

    }

    public override void runCurrentStateOnTriggerEnter(Collider other, PlayerStateController controller)
    {

    }


    //switch case to determine dashing state and whatnot
    //case 0: not dashing. in this phase check for space input, if pressed then face direction of movement and set velocity in that direction
    //case 1: run a timer for how long the dash will last. when the duration is over, set dash state back so that moment is stopped again
    //case -1: cooldown window. set a timer and when it is over, reset back to stage 0
      PlayerState performDash(float horiz, float vert){
          switch (dashState)
                 {
                 case 0:
                      //     PSM.pa.chargeAttack = false;
                      //     PSM.pa.holdTimer = 0f;
                           PSM.priority = 10;
                      //     PSM.pa.blockState = false;
                        //   PSM.pa.comboStep = 0;
                           //get the input data and normalize it to have a direction vector. then simply multiply it with speed. also look in direction of the dash which is just the normalized direction vector.
                           Vector3 fVelocity = new Vector3(horiz, PSM.rb.velocity.y, vert).normalized;
                           if (horiz == 0f && vert == 0f) {
                             fVelocity = new Vector3(transform.forward.x, PSM.rb.velocity.y, transform.forward.z).normalized;
                           }
                           PSM.rb.velocity = fVelocity * PSM.baseSpeed.Value * dashMultiplier;
                           transform.root.GetChild(0).rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(fVelocity), 1F);
                           PSM.numDashes -= 1;
                           dashState = 1;

                         return this;
                     break;
                 case 1:
                     dashTimer += Time.deltaTime;
                     if(dashTimer >= maxDash)
                     {
                         dashTimer = 0;
                         dashState = 0; //no longer dashing
                         PSM.rb.velocity = new Vector3(0,PSM.rb.velocity.y,0); //stop after dash ends
                         PSM.priority = 0;
                         return MovementState;
                     }
                     return this;
                     break;
                 }
        return this;
      }
}
