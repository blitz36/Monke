using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackState : State
{
    [SerializeField] private ChaseState chaseState;

    public override State runCurrentStateUpdate(StateController controller)
    {
        if (controller.isInKnockback)
        {
            Vector3 knockbackDirection = (controller.transform.position - controller.toChase.transform.position).normalized;
            knockbackDirection.y = 0f;
            //Debug.Log(knockbackDirection);
            controller.myRigidbody.AddForce(knockbackDirection * 10, ForceMode.Impulse);
            controller.isInKnockback = false;
        }

        controller.currentKnockbackTimer -= Time.deltaTime;

        if (controller.currentKnockbackTimer <= 0)
        {
            controller.isInKnockback = false;
            controller.currentKnockbackTimer = controller.knockbackTimer;
            return chaseState;
        }
        else
        {
            return this;
        }
    }

}
