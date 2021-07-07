using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attacks/EnemyAttack")]
public class EnemyAttacks : EnemyAttack
{
    public int State = 0;
    public float Timer;
    private GameObject Hitbox;
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    public float momentum;

    public override void Cancel() {
      Timer = 0f;
      State = 0;
      Hitbox.SetActive(false);
    }

    public override void createHitbox(Transform Player) {
      foreach (GameObject hitbox in hitboxes) {
        Hitbox = Instantiate(hitbox);
        Hitbox.transform.parent = Player;
        Hitbox.transform.localPosition = new Vector3(0,0,0);

        Hitbox.SetActive(false);
      }

    }
    public override void PerformAttack(ref bool attacking, Rigidbody rb, ref float cooldownTimer, float cooldownTime, Transform target) {
      switch (State) {
        case 0: //Starting/idle state


          if(attacking == true) //if slashing or a slash is buffered then perform the action
            {
              rb.velocity = new Vector3(0f,0f,0f);
              Hitbox.SetActive(true);
              State = 1;
              Timer = 0;
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
          Timer += Time.deltaTime;
          if(Timer >= activeTime)
          {
              Timer = 0f;
              State = -1;
              Hitbox.SetActive(false);

          }
          break;

        case -1: //recovery

          //timer to reset to the next combostep and reset the transforms
          Timer += Time.deltaTime;
          if (Timer >= recoveryTime) {
            Timer = 0f;
            State = 0;
            cooldownTimer = cooldownTime;
            attacking = false;
          }
          break;
      }
    }
}
