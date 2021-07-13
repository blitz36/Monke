using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Equipment/BFG")]
public class Equip_BFG : Equipable
{
    private int State;
    private float Timer;
    private List<GameObject> laser;

    public override void Cancel() {
      Timer = 0f;
      State = 0;
      foreach (GameObject hitbox in laser) {
        hitbox.SetActive(false);
      }
    }

    public override List<GameObject> createHitbox(Transform Player) {
      laser.Clear();
      foreach (GameObject hitbox in hitboxes) {
        laser.Add(Instantiate(hitbox));
        laser[laser.Count-1].transform.parent = Player;
        laser[laser.Count-1].transform.localPosition = new Vector3(0,0,0);
        laser[laser.Count-1].SetActive(false);

      }
      return laser;

    }
    public override void Activate(Rigidbody rb, Plane plane, GameObject gameObject, ref int priority) {
      switch (State) {
        case 0: //Starting/idle state


          if (Input.GetKeyDown(KeyCode.LeftShift)) { //if using item
              //dashing in the direction of the mouse for some momentum. raycast to a floor, then add force ein that direction
              rb.velocity = new Vector3(0, 0, 0);
              var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              float enter;
              if (plane.Raycast(ray, out enter))
              {
                  var hitPoint = ray.GetPoint(enter);
                  var mouseDir = hitPoint - gameObject.transform.position;
                  mouseDir = mouseDir.normalized;
                  rb.AddForce(mouseDir * -300);
                  gameObject.transform.LookAt (hitPoint);
                  gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y,0);

                  laser[0].SetActive(true);
                  State = 1;
                  Timer = 0;
                  priority = 3;
          }
        }
        break;

        case 1: //start up
        //decelerate the momentum during startup
        rb.velocity = rb.velocity * .98f;

        //timer to switch to active frames
          Timer += Time.deltaTime;
          if (Timer >= 0.1166f) {
            Timer = 0;
            State = 2;
          }
          break;

        case 2: //Active
          //stop all momentum at this point
          rb.velocity = new Vector3(0f,0f,0f);


          //timer before switching to recovery stage
          Timer += Time.deltaTime;
          if(Timer >= 1)
          {
              Timer = 0f;
              State = -1;
              laser[0].SetActive(false);
          }
          break;

        case -1: //recovery

          //timer to reset to the next combostep and reset the transforms
          Timer += Time.deltaTime;
          if (Timer >= 1) {
            Timer = 0f; //in reference to the combo attack system
            State = 0;
            priority = 0;
          }
          break;
      }
    }
}
