using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager_Player : MonoBehaviour
{
    private PlayerMovement PM;
    private playerStatManager PSM;
    private PlayerAttacks PA;
    private Animator animator;
    void Start()
    {
        PM = gameObject.GetComponentInParent<PlayerMovement>();
        PSM = gameObject.GetComponentInParent<playerStatManager>();
        PA = gameObject.GetComponentInParent<PlayerAttacks>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("horiz", PM.horiz);
        animator.SetFloat("vert", PM.vert);
        animator.SetInteger("isAction", PSM.priority);
        animator.SetBool("BlockState", PA.blockState);
        animator.SetInteger("ComboStep", PA.comboStep);

        if (PM.horiz != 0f || PM.vert != 0f) {
          animator.SetBool("isRunning", true);
        }
        else {
          animator.SetBool("isRunning", false);
        }

    }
}
