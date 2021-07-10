using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private float alarmRange;
    [SerializeField] private float triggerRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private ActivateState activateState;
    private bool isInTriggerRange = false;
    private bool isInAlertRange = false;
    

    public override State runCurrentStateUpdate(StateController controller)
    {
        if (!isInAlertRange)
        {
            controller.lightLerpSpeed = 1f;
        }
        else if (isInAlertRange && !isInTriggerRange)
        {
            controller.lightLerpSpeed = 10f;
        }

        flashing(controller);

        //Debug.Log("Now in idle state!");
        if (isInTriggerRange)
        {
            return activateState;
        }
        else
        {
            return this;
        }
    }

    public override void runCurrentStateFixedUpdate(StateController controller)
    {
        isInAlertRange = Physics.CheckSphere(transform.position, alarmRange, playerLayer);
        isInTriggerRange = Physics.CheckSphere(transform.position, triggerRange, playerLayer);
    }

    private void flashing(StateController controller)
    {
        if (controller.lightLerpDirection)
        {
            controller.lightLerpTimer += Time.deltaTime * controller.lightLerpSpeed;
        }
        else
        {
            controller.lightLerpTimer -= Time.deltaTime * controller.lightLerpSpeed;
        }
        controller.statusLight.intensity = Mathf.Lerp(0f, 1f, controller.lightLerpTimer);
        if (controller.lightLerpTimer <= 0f)
        {
            controller.lightLerpDirection = true;
        }
        if (controller.lightLerpTimer >= 1f)
        {
            controller.lightLerpDirection = false;
        }
    }
}
