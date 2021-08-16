using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    [SerializeField] private KnockbackState knockbackState;
    [SerializeField] private DetonateState detonateState;
    [SerializeField] private float detonateRange;
    [SerializeField] private LayerMask playerLayer;
    private bool isDetonate = false;
    private AStarPath pathfinding;

    public override void Awake(){
      base.Awake();
      pathfinding = gameObject.GetComponent<AStarPath>();
    }

    public override State runCurrentStateUpdate(StateController controller)
    {
        if (ESM.isHit == true) {
          return hitstunState;
        }
        base.runCurrentStateUpdate(controller);
        Vector3 pathDir = pathfinding.calculateDir(ESM.target);
        controller.myRigidbody.velocity = pathDir*10;

        if (controller.isInKnockback)
        {
            controller.myRigidbody.velocity = pathDir*10;
            return knockbackState;
        }
        else if (isDetonate)
        {
            return detonateState;
        }
        else
        {
            return this;
        }
    }

    public override void runCurrentStateFixedUpdate(StateController controller)
    {
        isDetonate = Physics.CheckSphere(transform.position, detonateRange, playerLayer);
    }

    public override void runCurrentStateOnTriggerEnter(Collider other, StateController controller)
    {
        //Debug.Log(other.name);
        if (other.tag == "Weapon")
        {
            Debug.Log("Weapon hit!");
            controller.isInKnockback = true;
            controller.currentKnockbackTimer = controller.knockbackTimer;
        }
        Vector3 pathDir = pathfinding.calculateDir(ESM.target);
        controller.myRigidbody.velocity = pathDir*10;
    }
}
