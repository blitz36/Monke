using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
  private MovementAgent MA;
  public float turnSpeed;

  public float timeInbetweenAttacks;
  private bool canAttack = true;
  private AttackState attackState;

  public float attackRange;
  private bool isInAttackRange;
  [SerializeField] private LayerMask playerLayer;

  public override void Awake() {
    base.Awake();
    if (attackState == null) {
      attackState = gameObject.transform.parent.GetComponentInChildren<AttackState>();
    }
    if (MA == null) {
      MA = gameObject.GetComponent<MovementAgent>();
    }
  }

  public override State runCurrentStateUpdate(StateController controller)
  {
    if (ESM.isHit == true) {
      return hitstunState;
    }
    base.runCurrentStateUpdate(controller);
    if (isInAttackRange && canAttack) {
      canAttack = false;
      StartCoroutine("attackCooldown");
      return attackState;
    }

    return this;
  }

  public override void runCurrentStateFixedUpdate(StateController controller)
  {
    MA.moveToPlayer();
    ESM.gameObject.transform.rotation = Quaternion.Slerp(ESM.gameObject.transform.rotation, Quaternion.LookRotation(ESM.rb.velocity.normalized), turnSpeed);
    isInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
  }

  public override void runCurrentStateOnTriggerEnter(Collider other, StateController controller)
  {

  }

  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
    Gizmos.DrawWireSphere (transform.position, attackRange);
  }

  IEnumerator attackCooldown() {
    yield return new WaitForSeconds(timeInbetweenAttacks);
    canAttack = true;
  }

}
