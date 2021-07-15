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
    public override void PerformAttack(ref bool attacking, Rigidbody rb, ref float cooldownTimer, float cooldownTime, Transform target) {
      switch (State) {
        case 0: //Starting/idle state
          if(attacking == true) //if attacking
            {
              currentTarget = target;
              StartCoroutine(spawnBullet());
              attacking = false;
              cooldownTimer = cooldownTime;
          }
        break;
      }
    }

    IEnumerator spawnBullet()
    {
      currentBullets = 0;
      while (currentBullets < maxBullets) {
        gameObject.transform.LookAt(currentTarget);
        gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y,0);
        GameObject bullet = Instantiate(Hitbox, transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);
        currentBullets += 1;
        yield return new WaitForSeconds(timeBetweenShots);
      }
    }

}
