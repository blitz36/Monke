using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : EnemyAttack
{
    public Vector3 posOfHitbox = new Vector3(0,0,0);
    public int State = 0;
    public float Timer = 0f;
    private GameObject Hitbox;
    private GameObject hitboxIndicator;
    public GameObject hitboxIndicatorPrefab;
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    public float momentum;

    public bool targetRotation;

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
        Hitbox.transform.localPosition = posOfHitbox;

        Hitbox.SetActive(false);
      }
      hitboxIndicator = Instantiate(hitboxIndicatorPrefab);
      hitboxIndicator.transform.parent = Player;
      hitboxIndicator.transform.localPosition = posOfHitbox;

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
          if (targetRotation == true) {
            hitboxIndicator.transform.position = target.transform.position;
          }
        }
        rb.velocity = new Vector3(0f,0f,0f);
        if (targetRotation == true) {
          Hitbox.transform.LookAt(target);
        }
      }

      //increment
      Timer += Time.deltaTime;
      if (Timer >= startupTime) {
        if (hitboxIndicator) {
          hitboxIndicator.SetActive(false);
        }
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

      if (targetRotation == true) {

      Vector3 direction = (target.position - Hitbox.transform.position).normalized;

      //create the rotation we need to be in to look at the target
      Quaternion _lookRotation = Quaternion.LookRotation(direction);

      //rotate us over time according to speed until we are in the required rotation
      Hitbox.transform.rotation = Quaternion.Slerp(Hitbox.transform.rotation, _lookRotation, Time.deltaTime * 3);
//      Hitbox.transform.rotation = Quaternion.RotateTowards(Hitbox.transform.rotation, target.rotation, step);

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
