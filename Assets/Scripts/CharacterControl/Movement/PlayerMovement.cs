using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using characterStats;

public class PlayerMovement : MonoBehaviour
{
  //Relating towards movement and velocity
  Rigidbody rb;
  playerStatManager pStatManager;

  //Dashing related variables, used as a timer for recovery and how long the dash lasts for
  public static int dashState;
  public float dashTimer;
  public float maxDash;
  public float dashCooldown;

  //to prevent running while fighting
  public static bool isAction = false;

  //to look at mouse lol
  private Vector3 lookTarget;

  void Start ()
  {
      rb = GetComponent<Rigidbody>();
      pStatManager = gameObject.GetComponent<playerStatManager>();
  }

//UPDATE FRAME EVERY FRAME DO INPUT CHECKS
  void Update () {
    float horiz = Input.GetAxisRaw ("Horizontal");
    float vert = Input.GetAxisRaw ("Vertical");

    //runs a check to determine if player wants to dash and then performs it
    if (PlayerAttacks.blockState == false) {
      performDash(horiz, vert);
    }

    //checks for directional inputs and makes the player run in that direction
    performMovement(horiz, vert);
}

  public void onEquip() {

  }

  //You can only move while not dashing or isnt fighting isAction checks for fighting
  //normal running stuff
  void performMovement(float horiz, float vert) {
    if (!isAction && !PlayerAttacks.blockState){
      if (dashState != 1){
      //  lookAtMouse();//always look towards mouse

        //if there is any direction inputs, run in that direction
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
          Vector3 fVelocity = new Vector3(horiz, 0f, vert);
          rb.velocity = fVelocity.normalized * pStatManager.baseSpeed.Value;
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(fVelocity.normalized),0.2F);
        }

        else {
          rb.velocity = new Vector3(0,0,0); //no input = stand still
        }
      }
    }
  }

//switch case to determine dashing state and whatnot
//case 0: not dashing. in this phase check for space input, if pressed then face direction of movement and set velocity in that direction
//case 1: run a timer for how long the dash will last. when the duration is over, set dash state back so that moment is stopped again
//case -1: cooldown window. set a timer and when it is over, reset back to stage 0
  void performDash(float horiz, float vert){
    switch (dashState)
           {
           case 0:
               var isDashKeyDown = Input.GetKeyDown (KeyCode.Space);
               if(isDashKeyDown)
                 {
                   if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
                     PlayerAttacks.cancelAttackFunctions();
              //       SwingWeapon.weapon.transform.rotation = SwingWeapon.savedRotationSW;
                  //   SwingWeapon.weapon.transform.localPosition = SwingWeapon.savedPositionSW;

                     //get the input data and normalize it to have a direction vector. then simply multiply it with speed. also look in direction of the dash which is just the normalized direction vector.
                     Vector3 fVelocity = new Vector3(horiz, 0f, vert);
                     rb.velocity = fVelocity.normalized * pStatManager.baseSpeed.Value * 3;
                     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(fVelocity.normalized), 1F);
                   }
                   dashState = 1;
               }
               break;
           case 1:
               dashTimer += Time.deltaTime;
               if(dashTimer >= maxDash)
               {
                   dashTimer = 0;
                   dashState = -1; //no longer dashing
                   isAction = false;
                   rb.velocity = new Vector3(0,0,0); //stop after dash ends
               }
               break;
           case -1: // cooldown
               dashTimer += Time.deltaTime;
               if(dashTimer >= dashCooldown) //when recovery time is up reset everything so u can dash again :)
               {
                   dashTimer = 0;
                   dashState = 0;
               }
               break;
           }
  }

  //raycast the mouse position and make the transform look at it. change the angle to ignore x z so that its just pure rotation.
  void lookAtMouse(){
    var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast (ray, out hit)) {
      lookTarget = hit.point;
    }
    transform.LookAt (lookTarget);
    transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
  }
}
