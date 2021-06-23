using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    void OnCollisionStay(Collision Collider) {
      if (Input.GetKey(KeyCode.E)) {
        PickUp();
      }
    }

    void PickUp() {
      Inventory.instance.Equip(item);
      Destroy(gameObject);
    }
}
