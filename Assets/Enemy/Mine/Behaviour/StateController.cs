using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;

    public GameObject toChase;

    [HideInInspector] public GameObject myGameObject;
    [HideInInspector] public Rigidbody myRigidbody;

    [HideInInspector] public bool isInKnockback = false;
    public float knockbackTimer;
    public float currentKnockbackTimer;

    void Awake() {
      toChase = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    //    myRigidbody.useGravity = false;


    }

    void Update()
    {
        runStateMachine(this);
    }

    private void runStateMachine(StateController controller)
    {
        State nextState = currentState?.runCurrentStateUpdate(controller);

        if (nextState != null)
        {
            switchToNextState(nextState);
        }
    }

    private void switchToNextState(State nextState)
    {
        currentState = nextState;
    }

    private void FixedUpdate()
    {
        currentState?.runCurrentStateFixedUpdate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState?.runCurrentStateOnTriggerEnter(other, this);
    }
}
