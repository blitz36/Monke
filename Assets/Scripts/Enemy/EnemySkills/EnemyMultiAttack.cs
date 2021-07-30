using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultiAttack : EnemyAttack
{
    public List<Vector3> posOfHitbox = new List<Vector3>();
    public int State = 0;
    public float Timer = 0f;
    private List<GameObject> Hitbox = new List<GameObject>();
    private List<GameObject> hitboxIndicator = new List<GameObject>();

    public List<GameObject> hitboxIndicatorPrefab = new List<GameObject>();
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    public float momentum;

    public bool targetRotation;

    public override void Cancel() {
      Timer = 0f;
      State = 0;
      foreach (GameObject Hitbox in Hitbox){
        Hitbox.SetActive(false);
      }

      if (hitboxIndicator.Count > 0) {
        foreach (GameObject Indicator in hitboxIndicator){
          Indicator.SetActive(false);
        }
      }

    }

    public override void createHitbox(Transform Player) {
      int index = 0;
      foreach (GameObject hitbox in hitboxes) {
        Hitbox[index] = Instantiate(hitbox);
        Hitbox[index].transform.parent = Player;
        Hitbox[index].transform.localPosition = posOfHitbox[index];
        Hitbox[index].SetActive(false);
        index += 1;
      }

      index = 0;
      foreach (GameObject indicator in hitboxIndicatorPrefab) {
        hitboxIndicator[index] = Instantiate(indicator);
        hitboxIndicator[index].transform.parent = Player;
        hitboxIndicator[index].transform.localPosition = posOfHitbox[index];
        hitboxIndicator[index].SetActive(false);
      }

    }

    public void startUp(Rigidbody rb, Transform target) {
      if (State != 1) {
        return;
      }
      //things to do in beginning
      if (Timer <= 0f) {
        rb.velocity = new Vector3(0f,0f,0f);

        foreach (GameObject indicator in hitboxIndicator) {
          indicator.SetActive(true);
          if (targetRotation == true) {
            indicator.transform.position = target.transform.position;
          }
        }

        if (targetRotation == true) {
          foreach (GameObject hitbox in Hitbox) {
            hitbox.transform.LookAt(target);
          }
        }
      }

      //increment
      Timer += Time.deltaTime;
      if (Timer >= startupTime) {
        foreach (GameObject indicator in hitboxIndicator) {
          indicator.SetActive(false);
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
        foreach (GameObject hitbox in Hitbox) {
          hitbox.SetActive(true);
        }
      }

      if (targetRotation == true) {
        foreach (GameObject hitbox in Hitbox) {
          Vector3 direction = (target.position - hitbox.transform.position).normalized;
          Quaternion _lookRotation = Quaternion.LookRotation(direction);
          hitbox.transform.rotation = Quaternion.Slerp(hitbox.transform.rotation, _lookRotation, Time.deltaTime * 3);
        }
      }

      Timer += Time.deltaTime;
      if(Timer >= activeTime)
      {
          Timer = 0f;
          State = 3;
          foreach (GameObject hitbox in Hitbox) {
            hitbox.SetActive(false);
          }

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
