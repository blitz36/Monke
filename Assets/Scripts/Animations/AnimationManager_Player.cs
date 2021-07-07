using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager_Player : MonoBehaviour
{
    private PlayerMovement PM;
    private playerStatManager PSM;
    private Animator animator;
    void Start()
    {
        PM = gameObject.GetComponentInParent<PlayerMovement>();
        PSM = gameObject.GetComponentInParent<playerStatManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("horiz", PM.horiz);
        animator.SetFloat("vert", PM.vert);

    }
}
