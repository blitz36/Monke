using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attacks/Slam")]
public class Slam : Attack
{
    public int State;
    public float Timer;
    //private List<GameObject> slamHitbox = new List<GameObject>();
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    public float momentum;
    public playerStatManager pst;

    public override float totalTime() {
      return startupTime + activeTime + recoveryTime;
    }

    public override void Cancel(playerStatManager PSM) {
      Timer = 0f;
      State = 0;
      Time.timeScale = 1f;
      foreach (GameObject hitbox in PSM.heavyAttackHitboxes) {
        hitbox.SetActive(false);
      }
    }

    public override List<GameObject> createHitbox(Transform Player) {
      List<GameObject> slamHitbox = new List<GameObject>();
      if (slamHitbox.Count > 0) {
        slamHitbox.Clear();
      }
      foreach (GameObject hitbox in hitboxes) {
        slamHitbox.Add(Instantiate(hitbox));
        slamHitbox[slamHitbox.Count-1].transform.parent = Player;
        slamHitbox[slamHitbox.Count-1].transform.localPosition = new Vector3(0,0,0);
        slamHitbox[slamHitbox.Count-1].SetActive(false);
      }
      return slamHitbox;

    }
    public override int PerformAttack(playerStatManager PSM) {
      switch (State) {
        case 0: //Starting/idle state

          if (PSM.bufferedAttack) //if slashing or a slash is buffered then perform the action
            {
              //dashing in the direction of the mouse for some momentum. raycast to a floor, then add force ein that direction
        //      PSM.rb.velocity = new Vector3(0, 0, 0);
              PSM.rb.AddForce(Vector3.forward * momentum, ForceMode.Impulse);
              PSM.bufferedAttack = false;
              State = 1;
              PSM.priority = 2;
              Timer = 0;

          }
        break;

        case 1: //start up
        //decelerate the momentum during startup
        PSM.rb.velocity = PSM.rb.velocity * .97f;

        //timer to switch to active frames
          Timer += Time.deltaTime;
          if (Timer >= startupTime) {
            Timer = 0;
            State = 2;
            PSM.heavyAttackHitboxes[PSM.chargeAttackType].SetActive(true);
          }
          break;

        case 2: //Active
          //stop all momentum at this point

          PSM.rb.velocity = new Vector3(0f,0f,0f);


          //timer before switching to recovery stage
          Timer += Time.deltaTime;
          if(Timer >= activeTime)
          {
              Timer = 0f;
              State = -1;
              PSM.heavyAttackHitboxes[PSM.chargeAttackType].SetActive(false);
          }
          break;

        case -1: //recovery

          //timer to reset to the next combostep and reset the transforms
          Timer += Time.deltaTime;
          if (Timer >= recoveryTime) {
            Timer = 0f; //in reference to the combo attack system
            State = 0;
            PSM.priority = 0;
            PSM.chargeAttack = false;
          }
          break;
      }
      return State;
    }
}
