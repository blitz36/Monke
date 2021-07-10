using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public List<Item> items = new List<Item>(); //this is the Inventory
  //This section does singleton stuff in order to have an inventory present throughout everywhere.
  public playerStatManager pStatManager;
  public static Inventory instance;

  void Awake() {
    pStatManager = gameObject.GetComponent<playerStatManager>();
    if (instance != null) {
      Debug.LogWarning("More than 1 inventory found");
    }

    instance = this; //anyone can access the inventory at all times
  }

  void Update(){
    if (Input.GetKey(KeyCode.R)) {
      UnequipAll();
    }
  }


  public void Equip(Item item) {
    items.Add(item);
    item.Equip(pStatManager);
  }

  public void Unequip(Item item) {
    items.Remove(item);
    item.Unequip(pStatManager);
  }

  public void UnequipAll(){
    foreach(Item item in items){
        Unequip(item);
    }
  }

}
