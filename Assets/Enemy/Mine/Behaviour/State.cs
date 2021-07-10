using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract State runCurrentStateUpdate(StateController controller);
    public virtual void runCurrentStateFixedUpdate(StateController controller)
    {

    }
    public virtual void runCurrentStateOnTriggerEnter(Collider other, StateController controller)
    {
        
    }
}