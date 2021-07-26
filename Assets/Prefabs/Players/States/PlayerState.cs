using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public playerStatManager PSM;


    public virtual void Awake() {
      if (!PSM) {
        PSM = transform.root.GetComponentInChildren<playerStatManager>();
      }
    }

    public virtual PlayerState runCurrentStateUpdate(PlayerStateController controller) {
      return this;
    }

    public virtual void runCurrentStateFixedUpdate(PlayerStateController controller)
    {

    }
    public virtual void runCurrentStateOnTriggerEnter(Collider other, PlayerStateController controller)
    {

    }
}
