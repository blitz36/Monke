using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public EnemyStatManager ESM;
    [HideInInspector]
    public HitStunState hitstunState;
    [HideInInspector]
    public DeadState deadState;

    public virtual void Awake() {
      if (ESM == null) {
        ESM = gameObject.transform.parent.parent.GetComponent<EnemyStatManager>();
      }

      if (hitstunState == null) {
        hitstunState = gameObject.transform.parent.GetComponentInChildren<HitStunState>();
      }

      if (deadState == null) {
        deadState = gameObject.transform.parent.GetComponentInChildren<DeadState>();
      }
    }

    public virtual State runCurrentStateUpdate(StateController controller) {
      if (ESM.isHit == true) {
        return hitstunState;
      }

      if (ESM.currentHealth <= 0) {
        return deadState;
      }

      return this;
    }

    public virtual void runCurrentStateFixedUpdate(StateController controller)
    {

    }
    public virtual void runCurrentStateOnTriggerEnter(Collider other, StateController controller)
    {

    }
}
