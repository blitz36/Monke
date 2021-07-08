using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private ItemPickup instance;

    void Start() {
      instance = this;
    }

    void OnTriggerStay(Collider Collider) {
      Debug.Log("collsions happening " + instance);
      if (Input.GetKey(KeyCode.E)) {
        PickUp(instance);
      }
    }

    void PickUp(ItemPickup identity) {
      if (identity == instance) {
        Inventory.instance.Equip(item);
        Destroy(gameObject);
      }
    }
}
