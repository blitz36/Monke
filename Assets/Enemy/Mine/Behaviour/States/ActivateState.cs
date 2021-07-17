using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActivateState : State
{
    [SerializeField] private ChaseState chaseState;

    private bool activated = false;
    public bool isGrounded = false;

    public override State runCurrentStateUpdate(StateController controller)
    {
        //Debug.Log("Now in activate state!");
        if (!activated)
        {
            activate(controller);
        }

        if (activated && isGrounded)
        {
            controller.myRigidbody.velocity = new Vector3(0, 0, 0);
            return chaseState;
        }
        else
        {
            return this;
        }
    }

    public override void runCurrentStateFixedUpdate(StateController controller)
    {
        if (activated && !isGrounded)
        {
            if (controller.myRigidbody.velocity.y < 0)
            {
                isGrounded = Physics.Raycast(transform.position, Vector3.down, .5f);
                //Debug.Log("Grounded: " + isGrounded);
            }
        }
    }

    private void activate(StateController controller)
    {
        controller.myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Launch from ground
        controller.myRigidbody.useGravity = true;
        controller.myRigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        //Debug.Log("Launched");
        activated = true;
    }
}
