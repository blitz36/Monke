using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
  private PlayerMovementState MovementState;
  private PlayerHeavyAttackState heavyAttackState;
  public float dashMultiplier = 5;
  public float dashTimer;
  public float maxDash;
  PlayerState state;
  Vector2 direction;
  public ParticleSystem  trail1;
  public ParticleSystem  trail2;
    // Start is called before the first frame update
    public override void Awake()
    {
      base.Awake();
      if (MovementState == null) {
        MovementState = gameObject.transform.root.GetComponentInChildren<PlayerMovementState>();
      }
      if (heavyAttackState == null) {
        heavyAttackState = gameObject.transform.root.GetComponentInChildren<PlayerHeavyAttackState>();
      }
    }

    public override PlayerState runCurrentStateUpdate(PlayerStateController controller)
    {
      direction = PSM.playerInput.Base.Move.ReadValue<Vector2>();
      state = performDash(direction.x, direction.y);
      if (PSM.playerInput.Base.HeavyAttack.triggered) {
        PSM.bufferedAttack = true;
        PSM.chargeAttack = true;
      }

      if (PSM.bufferedAttack && PSM.chargeAttack) {
        PSM.isRunning = false;
        return heavyAttackState;
      }

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
          switch (PSM.dashState)
                 {
                 case true:
                      //     PSM.pa.chargeAttack = false;
                      //     PSM.pa.holdTimer = 0f;
                           PSM.priority = 10;
                      //     PSM.pa.blockState = false;
                        //   PSM.pa.comboStep = 0;
                           //get the input data and normalize it to have a direction vector. then simply multiply it with speed. also look in direction of the dash which is just the normalized direction vector.
                           Vector3 fVelocity = new Vector3(horiz, 0, vert);
                           Transform cameraTransform = Camera.main.transform;
                           cameraTransform.eulerAngles = new Vector3(0f, cameraTransform.eulerAngles.y, cameraTransform.eulerAngles.z);
                           fVelocity = cameraTransform.TransformDirection(fVelocity);
                           transform.root.GetChild(0).rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(fVelocity.normalized), 1F);
                        //   fVelocity.y = PSM.rb.velocity.y;
                //           if (horiz == 0f && vert == 0f) {
                  //           fVelocity = new Vector3(transform.forward.x, PSM.rb.velocity.y, transform.forward.z).normalized;
                    //       }
                           PSM.rb.velocity = fVelocity * PSM.baseSpeed.Value * dashMultiplier;
                           trail1.Play();
                           trail2.Play();
                           PSM.numDashes -= 1;
                           PSM.dashState = false;

                         return this;
                 case false:
                     dashTimer += Time.deltaTime;
                     if(dashTimer >= maxDash)
                     {
                         dashTimer = 0;
                         PSM.dashState = true; //no longer dashing
                         PSM.rb.velocity = new Vector3(0,PSM.rb.velocity.y,0); //stop after dash ends
                         PSM.priority = 0;
                         trail1.Stop();
                         trail2.Stop();
                         return MovementState;
                     }
                     return this;
                 }
        return this;
      }
}
