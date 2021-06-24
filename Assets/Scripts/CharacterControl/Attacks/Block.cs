using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attacks/Block")]
public class Block : Attack
{
    public int State;
    public float Timer;
    private GameObject blockHitbox;
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    public float momentum;

    public override void Cancel() {
      Timer = 0f;
      State = 0;
      PlayerMovement.isAction = false;
      blockHitbox.SetActive(false);
      PlayerAttacks.blockState = false;
    }

    public override void createHitbox(Transform Player) {
      foreach (GameObject hitbox in hitboxes) {
        blockHitbox = Instantiate(hitbox);
        blockHitbox.transform.parent = Player;
        blockHitbox.transform.localPosition = new Vector3(0,0,0);

        blockHitbox.SetActive(false);
      }

    }
    public override void PerformAttack(Rigidbody rb, Plane plane, GameObject gameObject, ref bool blockState, ref bool isAction, ref int comboStep, int nextStep) {
      switch (State) {
        case 0: //Starting/idle state


          if(Input.GetMouseButton(1)) //if slashing or a slash is buffered then perform the action
            {
              //dashing in the direction of the mouse for some momentum. raycast to a floor, then add force ein that direction
              rb.velocity = new Vector3(0, 0, 0);
              var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              float enter;
              if (plane.Raycast(ray, out enter))
              {
                  var hitPoint = ray.GetPoint(enter);
                  var mouseDir = hitPoint - gameObject.transform.position;
                  mouseDir = mouseDir.normalized;
                  rb.AddForce(mouseDir * momentum);
                  gameObject.transform.LookAt (hitPoint);
                  gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y,0);

              blockHitbox.SetActive(true);
              blockState = true;
              State = 1;
              Timer = 0;
          }
        }
        break;

        case 1: //start up
        //decelerate the momentum during startup
        rb.velocity = rb.velocity * .97f;

        //timer to switch to active frames
          Timer += Time.deltaTime;
          if (Timer >= startupTime) {
            Timer = 0;
            State = 2;
          }
          break;

        case 2: //Active
          //stop all momentum at this point
          rb.velocity = new Vector3(0f,0f,0f);


          //timer before switching to recovery stage
          if(!Input.GetMouseButton(1))
          {
              Timer = 0f;
              State = -1;
              blockHitbox.SetActive(false);

          }
          break;

        case -1: //recovery

          //timer to reset to the next combostep and reset the transforms
          Timer += Time.deltaTime;
          if (Timer >= recoveryTime) {
            Timer = 0f; //in reference to the combo attack system
            State = 0;
            comboStep = nextStep;
            blockState = false;
          }
          break;
      }
    }
}
