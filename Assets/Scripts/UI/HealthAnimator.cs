using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAnimator : MonoBehaviour
{
  private playerStatManager PSM;
  private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
      if (PSM == null){
        PSM = transform.root.GetComponentInChildren<playerStatManager>();
      }
      if (animator == null) {
        animator = GetComponent<Animator>();
      }
    }

    // Update is called once per frame
    void Update()
    {
      if (PSM.numShields > 0) {
        animator.SetBool("NoShield", false);
      }
      else {
        animator.SetBool("NoShield", true);
      }
    }
}
