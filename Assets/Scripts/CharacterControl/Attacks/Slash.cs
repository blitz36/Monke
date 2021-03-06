using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attacks/Slash")]
public class Slash : Attack
{
    public int State;
    private float Timer;
  //  public List<GameObject> slashHitbox = new List<GameObject>();
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    public float momentum;
    private Vector3 mouseDir;
    public playerStatManager pst;

    public bool stopVelocityStartup;
    public bool stopVelocityActive;
    public bool stopVelocityRecovery;

    public override float totalTime() {
      return startupTime + activeTime + recoveryTime;
    }

    public override void Cancel(playerStatManager PSM) {
      Timer = 0f;
      State = 0;
      foreach (GameObject hitbox in PSM.lightAttackHitboxes) {
        hitbox.SetActive(false);
      }
    }

    public override List<GameObject> createHitbox(Transform Player) {
      List<GameObject> slashHitbox = new List<GameObject>();
      if (slashHitbox.Count > 0){
        slashHitbox.Clear();
      }

      foreach (GameObject hitbox in hitboxes) {
        GameObject box = Instantiate(hitbox);

        slashHitbox.Add(box);
        slashHitbox[slashHitbox.Count-1].transform.parent = Player;
        slashHitbox[slashHitbox.Count-1].transform.localPosition = new Vector3(0,0,0);
        slashHitbox[slashHitbox.Count-1].SetActive(false);

      }
      return slashHitbox;

    }
    public override int PerformAttack(playerStatManager PSM) {
      switch (State) {
        case 0: //Starting/idle state
              //dashing in the direction of the mouse for some momentum. raycast to a floor, then add force ein that direction
              var ray = Camera.main.ScreenPointToRay(PSM.playerInput.Base.MousePosition.ReadValue<Vector2>());
              float enter;
              if (PSM.plane.Raycast(ray, out enter))
              {
                  var hitPoint = ray.GetPoint(enter);
                  mouseDir = hitPoint - PSM.gameObject.transform.position;
                  mouseDir = mouseDir.normalized;
                  PSM.gameObject.transform.LookAt (hitPoint);
                  PSM.gameObject.transform.eulerAngles = new Vector3(0, PSM.gameObject.transform.eulerAngles.y,0);
                  PSM.bufferedAttack = false;
                  PSM.priority = 1;
                  State = 1;
                  Timer = 0;
                  if (stopVelocityStartup) {
                    PSM.rb.velocity = new Vector3(0f,0f,0f);
                  }
            }
        break;

        case 1: //start up
        //decelerate the momentum during startup
        PSM.rb.velocity = PSM.rb.velocity * .9f;
        //timer to switch to active frames
          Timer += Time.deltaTime;
          if (Timer >= startupTime) {
            Timer = 0;
            State = 2;
            PSM.rb.AddForce(mouseDir * momentum, ForceMode.Impulse);
            if (stopVelocityActive) {
              PSM.rb.velocity = new Vector3(0f,0f,0f);
            }
            float roll = Random.value;
             if (roll < PSM.critChancePerc.Value) {
               PSM.lightAttackHitboxes[PSM.comboStep].GetComponent<HitboxController>().isCrit = true;
             }
            PSM.lightAttackHitboxes[PSM.comboStep].SetActive(true);
          }
          break;

        case 2: //Active
          //stop all momentum at this point
          PSM.rb.velocity = PSM.rb.velocity * .96f;

          //timer before switching to recovery stage
          Timer += Time.deltaTime;
          if(Timer >= activeTime)
          {
              Timer = 0f;
              State = -1;
              PSM.lightAttackHitboxes[PSM.comboStep].SetActive(false);
              if (stopVelocityRecovery) {
                PSM.rb.velocity = new Vector3(0f,0f,0f);
              }
          }
          break;

        case -1: //recovery
          Timer += Time.deltaTime;
          if (Timer >= recoveryTime) {
            Timer = 0f; //in reference to the combo attack system
            State = 0;
            PSM.priority = 0;
          }
          break;
      }
      return State;
    }
}
