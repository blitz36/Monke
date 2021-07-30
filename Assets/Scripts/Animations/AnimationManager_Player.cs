using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager_Player : MonoBehaviour
{
    private playerStatManager PSM;
    private Animator animator;
    void Start()
    {
        PSM = gameObject.GetComponentInParent<playerStatManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("HoldTimer", PSM.holdTimer);
        animator.SetInteger("isAction", PSM.priority);
        animator.SetInteger("SlamType", PSM.chargeAttackType);
        animator.SetBool("BlockState", PSM.blockState);
        animator.SetInteger("ComboStep", PSM.comboStep);
        animator.SetBool("isRunning", PSM.isRunning);
        animator.SetBool("isHit", PSM.isHit);
        animator.SetBool("isParry", PSM.parried);


    }
}
