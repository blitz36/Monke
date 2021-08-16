using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Equipment/BFG")]
public class Equip_BFG : Equipable
{
    private float Timer;
    private List<GameObject> laser = new List<GameObject>();
    public float startTime;
    public float activeTime;
    public float recoveryTime;

    private Vector3 mouseDir;
    public override void Cancel() {
      Timer = 0f;
      State = 0;
      foreach (GameObject hitbox in laser) {
        hitbox.SetActive(false);
      }
    }

    public override List<GameObject> createHitbox(Transform Player) {
      if (laser.Count > 0) {
        laser.Clear();
      }
      foreach (GameObject hitbox in hitboxes) {
        laser.Add(Instantiate(hitbox));
        laser[laser.Count-1].transform.parent = Player;
        laser[laser.Count-1].transform.localPosition = new Vector3(0,0,0);
        laser[laser.Count-1].SetActive(false);

      }
      return laser;

    }
    public override int Activate(playerStatManager PSM) {
      switch (State) {
        case 0: //Starting/idle state
              //dashing in the direction of the mouse for some momentum. raycast to a floor, then add force ein that direction
          //    PSM.rb.velocity = new Vector3(0, 0, 0);
              var ray = Camera.main.ScreenPointToRay(PSM.playerInput.Base.MousePosition.ReadValue<Vector2>());
              float enter;
              if (PSM.plane.Raycast(ray, out enter))
              {
                  var hitPoint = ray.GetPoint(enter);
                  mouseDir = hitPoint - PSM.gameObject.transform.position;
                  mouseDir = mouseDir.normalized;

                  PSM.gameObject.transform.LookAt (hitPoint);
                  PSM.gameObject.transform.eulerAngles = new Vector3(0, PSM.gameObject.transform.eulerAngles.y,0);


                  State = 1;
                  Timer = 0;
                  PSM.priority = 3;
          }

        break;

        case 1: //start up
        //decelerate the momentum during startup
        PSM.rb.velocity = PSM.rb.velocity * .85f;

        //timer to switch to active frames
          Timer += Time.deltaTime;
          if (Timer >= startTime) {
            PSM.rb.AddForce(mouseDir * -900);
            laser[0].SetActive(true);
            Timer = 0;
            State = 2;
          }
          break;

        case 2: //Active
          //stop all momentum at this point
  //        PSM.rb.velocity = new Vector3(0f,0f,0f);


          //timer before switching to recovery stage
          Timer += Time.deltaTime;
          if(Timer >= activeTime)
          {
              Timer = 0f;
              State = -1;
              laser[0].SetActive(false);
          }
          break;

        case -1: //recovery

          //timer to reset to the next combostep and reset the transforms
          Timer += Time.deltaTime;
          if (Timer >= recoveryTime) {
            Timer = 0f; //in reference to the combo attack system
            PSM.priority = 0;
            State = 0;

          }
          break;
      }
      return State;
    }
}
