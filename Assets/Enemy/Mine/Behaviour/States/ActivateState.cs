using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActivateState : State
{
    public ChaseState chaseState;

    private NavMeshAgent myNMAgent;
    private Rigidbody myRigidbody;

    private bool activated = false;
    private bool isGrounded = false;

    private void Start()
    {
        myNMAgent = GetComponentInParent<NavMeshAgent>();
        myRigidbody = GetComponentInParent<Rigidbody>();
    }

    public override State runCurrentState()
    {
        Debug.Log("Now in activate state!");
        if (!activated)
        {
            activate();
        }

        if (activated && isGrounded)
        {
            myNMAgent.enabled = true;
            myRigidbody.isKinematic = true;
            return chaseState;
        }
        else
        {
            return this;
        }
    }

    private void FixedUpdate()
    {
        if (activated && !isGrounded)
        {
            if (myRigidbody.velocity.y < 0)
            {
                isGrounded = Physics.Raycast(transform.position, Vector3.down, .5f);
                //Debug.Log("Grounded: " + isGrounded);
            }
        }
    }

    private void activate()
    {
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Launch from ground
        myRigidbody.useGravity = true;
        myRigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        //Debug.Log("Launched");
        activated = true;
    }
}
