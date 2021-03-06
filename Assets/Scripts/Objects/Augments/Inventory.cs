using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public List<Item> items = new List<Item>(); //this is the Inventory for augments

  public List<GameObject> hitboxes = new List<GameObject>(); //list of hitboxes from all the attacks

  public List<Attack> lightAttack;
  public List<GameObject> lightAttackHitbox;

  public List<Attack> heavyAttack;
  public List<GameObject> heavyAttackHitbox;

  public Equipable equip;
  public List<GameObject> equipHitbox;

  public Block block;
  public List<GameObject> blockHitbox;

  //This section does singleton stuff in order to have an inventory present throughout everywhere.
  public playerStatManager pStatManager;
  public AugmentDisplayUI displayUI;
  public static Inventory instance;

  void Awake() {
    pStatManager = gameObject.GetComponent<playerStatManager>();
    if (instance != null) {
      Debug.LogWarning("More than 1 inventory found");
    }

    instance = this; //anyone can access the inventory at all times
    foreach (Attack attacks in lightAttack) {
      lightAttackHitbox.AddRange(attacks.createHitbox(transform));
    }
    foreach (Attack attacks in heavyAttack) {
      heavyAttackHitbox.AddRange(attacks.createHitbox(transform));
    }
    pStatManager.lightAttack.AddRange(lightAttack);
    pStatManager.heavyAttack.AddRange(heavyAttack);
    equipBlock(block);
    equipEquipmentAttack(equip);
  }

  void Update(){
  }

  public void Equip(Item item) {
    items.Add(item);
    item.Equip(pStatManager);
    displayUI.createDisplay(item);
  }

  public void Unequip(Item item) {
    items.Remove(item);
    item.Unequip(pStatManager);
    displayUI.decrementDisplay(item);
  }

  public void UnequipAll(){
    foreach(Item item in items){
        Unequip(item);
    }
  }

  public void refreshHitboxList() {
    hitboxes = new List<GameObject>(lightAttackHitbox); //make a new copy of lgiht attack hitbox list and then add in references to the rest.
    hitboxes.AddRange(heavyAttackHitbox);
    hitboxes.AddRange(equipHitbox);

    pStatManager.hitboxes = hitboxes;
    pStatManager.lightAttackHitboxes = lightAttackHitbox;
    pStatManager.heavyAttackHitboxes = heavyAttackHitbox;
    pStatManager.equipHitboxes = equipHitbox;
  }

  //clear hitboxes, then clear current light attacks, then equip new light attacks and create their hitboxes.
  public void equipLightAttack(List<Attack> attack) {
    foreach (GameObject hitbox in lightAttackHitbox) {
      Destroy(hitbox);
    }
    lightAttack.Clear();
    foreach (Attack attacks in attack) {
      lightAttack.Add(attacks);
      lightAttackHitbox.AddRange(attacks.createHitbox(transform));
    }
    refreshHitboxList();
    pStatManager.lightAttack.Clear();
    pStatManager.lightAttack.AddRange(lightAttack);
  }



  public void equipEquipmentAttack(Equipable equipment) {
    foreach (GameObject hitbox in equipHitbox) {
      Destroy(hitbox);
    }
    equip = equipment;
    equipHitbox.AddRange(equip.createHitbox(transform));
    pStatManager.equip = equip;
    refreshHitboxList();
  }

  public void equipHeavyAttack(List<Attack> attack) {
    foreach (GameObject hitbox in heavyAttackHitbox) {
      Destroy(hitbox);
    }

    heavyAttack.Clear();
    foreach (Attack attacks in attack) {
      heavyAttack.Add(attacks);
      heavyAttackHitbox.AddRange(attacks.createHitbox(transform));
    }
    refreshHitboxList();
    pStatManager.heavyAttack.Clear();
    pStatManager.heavyAttack.AddRange(heavyAttack);
  }

  public void equipBlock(Block attack) {
    foreach (GameObject hitbox in blockHitbox) {
      Destroy(hitbox);
    }
    block = attack;
    blockHitbox.AddRange(attack.createHitbox(transform));
    pStatManager.block = block;
    refreshHitboxList();
  }


}
