using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attacks/Block")]
public class Block : Attack
{
    public int State;
    public float Timer;
    private List<GameObject> blockHitbox = new List<GameObject>();
    private BlockHitbox bh;
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    public float momentum;

    public float returnParryTime() {
      return bh.releaseTime;
    }

    public override float totalTime() {
      return startupTime + activeTime + recoveryTime;
    }

    public override void Cancel() {
      Timer = 0f;
      State = 0;
      foreach (GameObject hitbox in blockHitbox) {
        hitbox.SetActive(false);
      }
    }

    public override List<GameObject> createHitbox(Transform Player) {
      if (blockHitbox.Count > 0){
        blockHitbox.Clear();
      }

      foreach (GameObject hitbox in hitboxes) {
        blockHitbox.Add(Instantiate(hitbox));
        blockHitbox[blockHitbox.Count-1].transform.parent = Player;
        blockHitbox[blockHitbox.Count-1].transform.localPosition = new Vector3(0,0,0);
        bh = blockHitbox[blockHitbox.Count-1].GetComponent<BlockHitbox>();
        blockHitbox[blockHitbox.Count-1].SetActive(false);
      }
      return blockHitbox;

    }
    public override void PerformAttack(Rigidbody rb, Plane plane, GameObject gameObject, ref bool blockState, ref int priority, ref int comboStep, int nextStep) {
      switch (State) {
        case 0: //Starting/idle state


          if(Input.GetMouseButton(1)) //if slashing or a slash is buffered then perform the action
            {
              //dashing in the direction of the mouse for some momentum. raycast to a floor, then add force ein that direction
          //    rb.velocity = rb.velocity.normalized;
              var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              float enter;
              if (plane.Raycast(ray, out enter))
              {
                  var hitPoint = ray.GetPoint(enter);
                  var mouseDir = hitPoint - gameObject.transform.position;
                  mouseDir = mouseDir.normalized;
        //          var direction = rb.velocity.normalized;
        //          direction.y = 0f;
        //          rb.AddForce(direction * momentum, ForceMode.Impulse);
                  gameObject.transform.LookAt (hitPoint);
                  gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y,0);

              blockHitbox[0].SetActive(true);
              blockState = true;
              priority = 11;
              State = 1;
              Timer = 0;
          }
        }
        break;

        case 1: //start up

        //timer to switch to active frames
          Timer += Time.deltaTime;
          if (Timer >= startupTime) {
            Timer = 0;
            State = 2;
          }
          break;

        case 2: //Active

          //timer before switching to recovery stage
          if(!Input.GetMouseButton(1))
          {
              Timer = 0f;
              State = -1;
              blockHitbox[0].SetActive(false);
              blockState = false;

          }
          break;

        case -1: //recovery

          //timer to reset to the next combostep and reset the transforms
          Timer += Time.deltaTime;
          if (Timer >= recoveryTime) {
            Timer = 0f; //in reference to the combo attack system
            State = 0;
            comboStep = nextStep;
            priority = 0;
          }
          break;
      }
    }
}
