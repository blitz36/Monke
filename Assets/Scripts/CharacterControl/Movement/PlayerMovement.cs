using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody rb;
  float speed = 13f;

  public int dashState;
  public float dashTimer;
  public float maxDash = 1f;
  public Vector3 savedVelocity;
  public Quaternion lastRotation;

  public static bool isAction = false;

  private Vector3 lookTarget;

  void Start ()
  {
      rb = GetComponent<Rigidbody>();
  }

  void Update () {
    float horiz = Input.GetAxisRaw ("Horizontal");
    float vert = Input.GetAxisRaw ("Vertical");

    //to perform dashes
    performDash(horiz, vert);

    //You can only move while not dashing
    //All the movement stuff
    if (!isAction){
        if (dashState != 1){
          lookAtMouse();
          if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            Vector3 fVelocity = new Vector3(horiz, 0f, vert);
            rb.velocity = fVelocity.normalized * speed;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(fVelocity.normalized), 0.02F);
        //    lastRotation = transform.rotation;
          }
          else {
        //    transform.rotation = Quaternion.Slerp(transform.rotation, lastRotation, 0.15F);
            rb.velocity = new Vector3(0,0,0);
          }
       }
   }
}

  void performDash(float horiz, float vert){
    switch (dashState)
           {
           case 0:
               var isDashKeyDown = Input.GetKeyDown (KeyCode.Space);
               if(isDashKeyDown)
                 {
                   if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
                     SwingWeapon.weapon.transform.rotation = SwingWeapon.savedRotationSW;
                     SwingWeapon.weapon.transform.localPosition = SwingWeapon.savedPositionSW;

                     Vector3 fVelocity = new Vector3(horiz, 0f, vert);
                     rb.velocity = fVelocity.normalized * speed * 3;
                     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(fVelocity.normalized), 0.02F);
                     lastRotation = transform.rotation;
                   }
                   dashState = 1;
               }
               break;
           case 1:
               dashTimer += Time.deltaTime * 3;
               if(dashTimer >= maxDash)
               {
                   dashTimer = maxDash;
                   GetComponent<Rigidbody>().velocity = savedVelocity;
              //     transform.rotation = Quaternion.LookRotation(savedVelocity);
                   dashState = -1;
                   SwingWeapon.cooldownTimer = 0f;
               }
               break;
           case -1:
               dashTimer -= Time.deltaTime;
               if(dashTimer <= 0)
               {
                   dashTimer = 0;
                   dashState = 0;
               }
               break;
           }
  }

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
