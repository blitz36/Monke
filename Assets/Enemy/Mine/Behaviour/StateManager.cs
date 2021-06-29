using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public State currentState;
    [SerializeField] GameObject myObject;

    private NavMeshAgent myNMAgent;
    private Rigidbody myRigidbody;

    void Start()
    {
        myNMAgent = myObject.GetComponent<NavMeshAgent>();
        myRigidbody = myObject.GetComponent<Rigidbody>();
        myNMAgent.enabled = false;
        myRigidbody.isKinematic = false;
        myRigidbody.useGravity = false;
    }

    void Update()
    {
        runStateMachine();
    }

    private void runStateMachine()
    {
        State nextState = currentState?.runCurrentState();

        if (nextState != null)
        {
            switchToNextState(nextState);
        }
    }

    private void switchToNextState(State nextState)
    {
        currentState = nextState;
    }
}
