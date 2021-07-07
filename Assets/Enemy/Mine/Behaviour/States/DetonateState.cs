using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonateState : State
{
    [SerializeField] private float countdownTimer;
    [SerializeField] private GameObject explosionEffect;

    public override State runCurrentStateUpdate(StateController controller)
    {
        if (countdownTimer > 0)
        {
            detonate(controller);
            Destroy(controller.gameObject);
            return null;
        }
        else
        {
            countdownTimer -= Time.deltaTime;
            return this;
        }
    }

    private void detonate(StateController controller)
    {
        Instantiate(explosionEffect, controller.myRigidbody.transform.position, controller.myRigidbody.transform.rotation);
    }
}
