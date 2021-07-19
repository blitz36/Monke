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
    private Transform currentTarget;

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
        StartCoroutine(spawnBullet());
      }
      if (State == -1) { //if done with spawning bullets, then reset state back to 0 and send back.
        State = 0;
      }
      return State;
    }

    IEnumerator spawnBullet()
    {
      currentBullets = 0;
      while (currentBullets < maxBullets) {
        if (State == 0) {
          yield break;
        }
        gameObject.transform.LookAt(currentTarget);
        gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y,0);

        GameObject bullet = Instantiate(Hitbox, transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);

        currentBullets += 1;
        yield return new WaitForSeconds(timeBetweenShots);
      }
      State = -1;
    }

}
