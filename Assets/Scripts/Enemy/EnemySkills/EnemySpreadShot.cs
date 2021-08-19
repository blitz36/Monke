using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpreadShot : EnemyAttack
{
    public int State = 0;
    public float Timer;
    public GameObject Hitbox;
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    public float momentum;
    public float speed;
    public int maxBullets;
    private int currentBullets;
    public float timeBetweenShots;
    public Transform currentTarget;

    public float attackAngle;

    public override void Cancel() {
      Timer = 0f;
      State = 0;
    }

    public override void createHitbox(Transform Player) {

    }
    public override int PerformAttack(Rigidbody rb, Transform target) {
      currentTarget = target;
      if (State == 0) {
        State = 1;

        StartCoroutine(spawnBullet(rb));
      }
      rb.velocity *= .9f;
      if (State == -1) { //if done with spawning bullets, then reset state back to 0 and send back.
        State = 0;
      }
      return State;
    }

    IEnumerator spawnBullet(Rigidbody rb)
    {
      currentBullets = 0;
      float currentAttackAngle = attackAngle * -1/2;

      rb.gameObject.transform.LookAt(currentTarget);
      float centeredAngle = rb.gameObject.transform.eulerAngles.y;
      while (currentBullets < maxBullets) {
        if (State == 0) {
          yield break;
        }
        rb.gameObject.transform.eulerAngles = new Vector3(0, centeredAngle + currentAttackAngle,0);
        GameObject bullet = Instantiate(Hitbox, transform.position, Quaternion.identity);
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.velocity = rb.transform.forward * speed;

        currentBullets += 1;
        currentAttackAngle += attackAngle /maxBullets;
        yield return new WaitForSeconds(timeBetweenShots);
      }
      State = -1;
    }

}
