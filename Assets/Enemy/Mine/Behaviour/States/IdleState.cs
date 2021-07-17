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
    private MineLights ML;

    void Start() {
      if (ML == null) {
        ML = gameObject.GetComponent<MineLights>();
      }
      if (activateState == null) {
        activateState = gameObject.transform.parent.GetComponentInChildren<ActivateState>();
      }
    }


    public override State runCurrentStateUpdate(StateController controller)
    {
        if (!isInAlertRange)
        {
            ML.lightLerpSpeed = 1f;
        }
        else if (isInAlertRange && !isInTriggerRange)
        {
            ML.lightLerpSpeed = 10f;
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
        if (ML.lightLerpDirection)
        {
            ML.lightLerpTimer += Time.deltaTime * ML.lightLerpSpeed;
        }
        else
        {
            ML.lightLerpTimer -= Time.deltaTime * ML.lightLerpSpeed;
        }
        ML.statusLight.intensity = Mathf.Lerp(0f, 1f, ML.lightLerpTimer);
        if (ML.lightLerpTimer <= 0f)
        {
            ML.lightLerpDirection = true;
        }
        if (ML.lightLerpTimer >= 1f)
        {
            ML.lightLerpDirection = false;
        }
    }
}
