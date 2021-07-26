using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideInputManager : MonoBehaviour
{
  public float triggerRange;
  public LayerMask interactableLayerMask;
  private playerStatManager PSM;
  private bool isInTriggerRange;

  void Awake() {
      if (PSM == null)
        PSM = gameObject.GetComponentInChildren<playerStatManager>();
    }

/*
  void OnTriggerEnter(Collider col) {
    col.gameObject.GetComponent<RandomLootGenerator>().itemDescription.gameObject.SetActive(true);
  }

  void OnTriggerExit(Collider col) {
    col.gameObject.GetComponent<RandomLootGenerator>().itemDescription.gameObject.SetActive(false);
  }
*/
  void OnTriggerStay(Collider Collider) {
    if (PSM.playerInput.Base.Interact.triggered) {
      Collider.gameObject.GetComponent<RandomLootGenerator>().PickUp();
    }

  }

    // Update is called once per frame
    void FixedUpdate()
    {
      isInTriggerRange = Physics.CheckSphere(transform.position, triggerRange, interactableLayerMask);
    }
}
