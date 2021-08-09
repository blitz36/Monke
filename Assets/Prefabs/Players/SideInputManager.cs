using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideInputManager : MonoBehaviour
{
  public Inputs playerInput;

  public float triggerRange;
  public LayerMask interactableLayerMask;
  private playerStatManager PSM;
  private bool isInTriggerRange;

  void Awake() {
      playerInput = new Inputs();

      if (PSM == null)
        PSM = gameObject.GetComponentInChildren<playerStatManager>();
    }

    private void OnEnable() {
        playerInput.Enable();
    }

    private void OnDisable() {
      playerInput.Disable();
    }

    void Start() {
      playerInput.Base.HeavyAttack.started += _ => PSM.holding = true;
      playerInput.Base.HeavyAttack.performed += _ => PSM.holding = false;
      playerInput.Base.HeavyAttack.canceled += _ => PSM.holding = false;

      playerInput.Base.Block.started += _ => PSM.blockTrigger = true;
      playerInput.Base.Block.performed += _ => PSM.blockTrigger = false;
      playerInput.Base.Block.canceled += _ => PSM.blockTrigger = false;
    }

    void Update(){
      holdInput();
    }

    void holdInput(){
      if (PSM.holding == false) {
        if (PSM.holdTimer > 0f) {
          if (PSM.holdTimer < PSM.holdTimes[0]) {
            PSM.chargeAttackType = 0;
          }
          else if (PSM.holdTimer < PSM.holdTimes[1]) {
            PSM.chargeAttackType = 1;
          }
          else {
            PSM.chargeAttackType = 2;
          }
          PSM.holdTimer = 0f;
        }
        return;
      }
      else {
        PSM.holdTimer += Time.deltaTime;
      }
    }

  void OnTriggerStay(Collider Collider) {
    if (PSM.playerInput.Base.Interact.triggered) {
      if (Collider.tag == "Augment") {
        Collider.gameObject.GetComponent<RandomLootGenerator>().PickUp();
      }
      else if (Collider.tag == "Exit") {
        Collider.gameObject.GetComponent<ExitScript>().leaveMap(PSM);
      }
    }

  }

    // Update is called once per frame
    void FixedUpdate()
    {
      isInTriggerRange = Physics.CheckSphere(transform.position, triggerRange, interactableLayerMask);
    }
}
