using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    [SerializeField] private KnockbackState knockbackState;
    [SerializeField] private DetonateState detonateState;

    public override State runCurrentStateUpdate(StateController controller)
    {
        if (controller.myNMAgent.isOnNavMesh)
        {
            controller.myNMAgent.SetDestination(controller.toChase.transform.position);
        }

        if (controller.isInKnockback)
        {
            if (controller.myNMAgent.isOnNavMesh)
            {
                controller.myNMAgent.SetDestination(controller.myNMAgent.transform.position);
            }
            return knockbackState;
        }
        else
        {
            return this;
        }
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

        //Re-enable nav mesh agent after knockback
        if (!controller.myNMAgent.enabled)
        {
            if (other.tag == "Terrain")
            {
                controller.myNMAgent.enabled = true;
                controller.myRigidbody.isKinematic = true;
            }
        }
    }
}
