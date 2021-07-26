using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStateController : MonoBehaviour
{
    public PlayerState currentState;
    public playerStatManager PSM;
//    [HideInInspector] public Rigidbody myRigidbody;

    void Awake() {
      if (PSM == null)
        PSM = gameObject.GetComponent<playerStatManager>();
    }

    void Start()
    {
  //      myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        runStateMachine(this);
    }

    private void runStateMachine(PlayerStateController controller)
    {
        PlayerState nextState = currentState?.runCurrentStateUpdate(controller);

        if (nextState != null)
        {
            switchToNextState(nextState);
        }
    }

    private void switchToNextState(PlayerState nextState)
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
