using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private float alarmRange;
    [SerializeField] private float triggerRange;
    [SerializeField] private LayerMask PlayerLayer;
    public ActivateState activateState;
    public bool isInTriggerRange = false;
    

    public override State runCurrentState()
    {
        if (isInTriggerRange)
        {
            return activateState;
        }
        else
        {
            return this;
        }
    }

    private void FixedUpdate()
    {
        if (!isInTriggerRange)
        {
            isInTriggerRange = Physics.CheckSphere(transform.position, triggerRange, PlayerLayer);
        }
    }
}
