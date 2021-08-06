using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private EnemyStatManager ESM;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
      ESM = gameObject.GetComponentInParent<EnemyStatManager>();
      animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      animator.SetInteger("AnimValues", ESM.currentAnim);
    }
}
