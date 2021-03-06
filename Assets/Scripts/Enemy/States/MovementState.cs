using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
  private MovementAgent MA;
  public float turnSpeed;

  public float timeInbetweenAttacks;
  public float minAttackTime;
  public float maxAttackTime;
  private bool canAttack = false;
  private CombatState attackState;
  private StunnedState stunnedState;
  private DefaultState defaultState;

  public float attackRange;
  private bool isInAttackRange;
  [SerializeField] private LayerMask playerLayer;

  public override void Awake() {
    base.Awake();
    StartCoroutine("attackCooldown");
    if (attackState == null) {
      attackState = gameObject.transform.parent.GetComponentInChildren<CombatState>();
    }
    if (stunnedState == null) {
      stunnedState = gameObject.transform.parent.GetComponentInChildren<StunnedState>();
    }
    if (defaultState == null) {
      defaultState = gameObject.transform.parent.GetComponentInChildren<DefaultState>();
    }
    if (MA == null) {
      MA = gameObject.GetComponent<MovementAgent>();
    }
  }

  public override State runCurrentStateUpdate(StateController controller)
  {
    ESM.currentAnim = 1;
    if (ESM.isHit) {
      return hitstunState;
    }

    if (ESM.stunned) {
      return stunnedState;
    }
    if (ESM.target == null) {
      return defaultState;
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
    MA.moveToPlayer(ESM.target);
    Vector3 lookDirection = new Vector3(ESM.rb.velocity.normalized.x, 0f, ESM.rb.velocity.normalized.z);
    if (lookDirection != Vector3.zero) {
      ESM.gameObject.transform.rotation = Quaternion.Slerp(ESM.gameObject.transform.rotation, Quaternion.LookRotation(lookDirection), turnSpeed);
    }
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
    float randomTime = Random.Range(minAttackTime, maxAttackTime);
    yield return new WaitForSeconds(randomTime);
    canAttack = true;
  }

}
