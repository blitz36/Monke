using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;

    public Light statusLight;
    public bool lightLerpDirection;
    public float lightLerpSpeed;
    public Color lightColor;
    [SerializeField] [Range(0f, 1f)] public float lightLerpTimer;

    public GameObject toChase;

    [HideInInspector] public GameObject myGameObject;
    [HideInInspector] public NavMeshAgent myNMAgent;
    [HideInInspector] public Rigidbody myRigidbody;

    [HideInInspector] public bool isInKnockback = false;
    public float knockbackTimer;
    public float currentKnockbackTimer;

    void Start()
    {
        myNMAgent = myGameObject.GetComponent<NavMeshAgent>();
        myRigidbody = myGameObject.GetComponent<Rigidbody>();

        myNMAgent.enabled = false;
        myRigidbody.isKinematic = false;
        myRigidbody.useGravity = false;

        //ColorUtility.TryParseHtmlString("#E40C0C", out lightColor);
        statusLight.color = lightColor;
        statusLight.intensity = 0;
        lightLerpDirection = true;
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
