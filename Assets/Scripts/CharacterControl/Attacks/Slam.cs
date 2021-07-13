using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attacks/Slam")]
public class Slam : Attack
{
    public int State;
    public float Timer;
    private List<GameObject> slamHitbox;
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    public float momentum;
    public playerStatManager pst;

    public override float totalTime() {
      return startupTime + activeTime + recoveryTime;
    }

    public override void Cancel() {
      Timer = 0f;
      State = 0;
      foreach (GameObject hitbox in slamHitbox) {
        hitbox.SetActive(false);
      }
    }

    public override List<GameObject> createHitbox(Transform Player) {
      slamHitbox.Clear();
      foreach (GameObject hitbox in hitboxes) {
        slamHitbox.Add(Instantiate(hitbox));
        slamHitbox[slamHitbox.Count-1].transform.parent = Player;
        slamHitbox[slamHitbox.Count-1].transform.localPosition = new Vector3(0,0,0);
        slamHitbox[slamHitbox.Count-1].SetActive(false);
      }
      return slamHitbox;

    }
    public override void PerformAttack(Rigidbody rb, Plane plane, GameObject gameObject, ref bool bufferAttack, ref int priority, ref int comboStep, int nextStep) {
      switch (State) {
        case 0: //Starting/idle state

          if (bufferAttack) //if slashing or a slash is buffered then perform the action
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
                  rb.AddForce(mouseDir * momentum, ForceMode.Impulse);
                  gameObject.transform.LookAt (hitPoint);
                  gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y,0);


              bufferAttack = false;
              State = 1;
              priority = 2;
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
            slamHitbox[0].SetActive(true);
          }
          break;

        case 2: //Active
          //stop all momentum at this point

          rb.velocity = new Vector3(0f,0f,0f);


          //timer before switching to recovery stage
          Timer += Time.deltaTime;
          if(Timer >= activeTime)
          {
              Timer = 0f;
              State = -1;
              slamHitbox[0].SetActive(false);
          }
          break;

        case -1: //recovery

          //timer to reset to the next combostep and reset the transforms
          Timer += Time.deltaTime;
          if (Timer >= recoveryTime) {
            Timer = 0f; //in reference to the combo attack system
            State = 0;
            priority = 0;
            comboStep = nextStep;
          }
          break;
      }
    }
}
