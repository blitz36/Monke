using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : EnemyAttack
{
    public int State = 0;
    public float Timer = 0f;
    private GameObject Hitbox;
    private GameObject hitboxIndicator;
    public GameObject hitboxIndicatorPrefab;
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    public float momentum;

    public override void Cancel() {
      Timer = 0f;
      State = 0;
      Hitbox.SetActive(false);
      if (hitboxIndicator) {
        hitboxIndicator.SetActive(false);
      }
    }

    public override void createHitbox(Transform Player) {
      foreach (GameObject hitbox in hitboxes) {
        Hitbox = Instantiate(hitbox);
        Hitbox.transform.parent = Player;
        Hitbox.transform.localPosition = new Vector3(0,0,0);

        Hitbox.SetActive(false);
      }
      hitboxIndicator = Instantiate(hitboxIndicatorPrefab);
      hitboxIndicator.transform.parent = Player;
      hitboxIndicator.transform.localPosition = new Vector3(0,0,0);

      hitboxIndicator.SetActive(false);

    }

    public void startUp(Rigidbody rb, Transform target) {
      if (State != 1) {
        return;
      }
      //things to do in beginning
      if (Timer <= 0f) {
        if (hitboxIndicator) {
          hitboxIndicator.SetActive(true);
        }
        rb.velocity = new Vector3(0f,0f,0f);
      }

      //increment
      Timer += Time.deltaTime;
      if (Timer >= startupTime) {
        hitboxIndicator.SetActive(false);
        Timer = 0;
        State = 2;
      }

    }

    public void active(Rigidbody rb, Transform target) {
      if (State != 2) {
        return;
      }

      if (Timer <= 0f) {
        Hitbox.SetActive(true);
      }

      Timer += Time.deltaTime;
      if(Timer >= activeTime)
      {
          Timer = 0f;
          State = 3;
          Hitbox.SetActive(false);

      }

    }

    public void recovery(Rigidbody rb, Transform target) {
      if (State != 3) {
        return;
      }

      Timer += Time.deltaTime;
      if (Timer >= recoveryTime) {
        Timer = 0f;
        State = 0;
      }

    }

    public override int PerformAttack(Rigidbody rb, Transform target) {
      if (State == 0) State = 1;
      startUp(rb, target);
      active(rb, target);
      recovery(rb, target);
      return State;
    }
}
