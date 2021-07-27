using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager_Player : MonoBehaviour
{
  //  private PlayerMovement PM;
    private playerStatManager PSM;
//    private PlayerAttacks PA;
    private Animator animator;
    void Start()
    {
    //    PM = gameObject.GetComponentInParent<PlayerMovement>();
        PSM = gameObject.GetComponentInParent<playerStatManager>();
    //    PA = gameObject.GetComponentInParent<PlayerAttacks>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
  //      animator.SetFloat("horiz", PM.horiz);
    //    animator.SetFloat("vert", PM.vert);
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
