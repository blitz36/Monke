using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float turnSpeed;
    public EnemyAttack slash;
    private MovementAgent ma;
    private EnemyStatManager est;
    private Transform target;
    public bool attacking = false;
    private Rigidbody rb;
    public float cooldownTimer = 10f;
    public float cooldownTime;
    void Awake() {
      est = gameObject.GetComponent<EnemyStatManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
      ma = gameObject.GetComponent<MovementAgent>();
      target = ma.targetPosition;
      rb = gameObject.GetComponent<Rigidbody>();
      slash.createHitbox(gameObject.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      if (est.isHit == true) {
        return;
      }
      if (attacking == false) {
        ma.moveToPlayer();
        this.gameObject.transform.GetChild(0).localRotation = Quaternion.Slerp(this.gameObject.transform.GetChild(0).localRotation, Quaternion.LookRotation(rb.velocity.normalized), turnSpeed);
      }

    }

    void Update() {
      if (est.isHit == true) {
        return;
      }

      float sqrLen = ma.sqrLen;
      cooldownTimer -= Time.deltaTime;
      if (sqrLen < 50f) {
        if (cooldownTimer <= 0) {
          attacking = true;
          // Hitbox.transform.LookAt(target);
          Vector3 relativePos = target.position - transform.position;
          // the second argument, upwards, defaults to Vector3.up
          relativePos.y = 0f;
          Quaternion rotation = Quaternion.Slerp(this.gameObject.transform.GetChild(0).localRotation,Quaternion.LookRotation(relativePos, Vector3.up), turnSpeed);
          this.gameObject.transform.GetChild(0).localRotation = rotation;
        }
      }

      if (attacking == true) {
        slash.PerformAttack(ref attacking, rb, ref cooldownTimer, cooldownTime, target);
      }
    }
}
