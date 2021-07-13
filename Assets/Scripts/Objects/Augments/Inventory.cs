using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public List<Item> items = new List<Item>(); //this is the Inventory for augments

  public List<GameObject> hitboxes = new List<GameObject>(); //list of hitboxes from all the attacks

  public List<Attack> lightAttack;
  public List<GameObject> lightAttackHitbox;

  public Attack heavyAttack;
  public List<GameObject> heavyAttackHitbox;

  public Equipable equip;
  public List<GameObject> equipHitbox;

  public Block block;
  public List<GameObject> blockHitbox;

  //This section does singleton stuff in order to have an inventory present throughout everywhere.
  public playerStatManager pStatManager;
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
    pStatManager.lightAttack.AddRange(lightAttack);
    equipBlock(block);
    equipHeavyAttack(heavyAttack);
    equipEquipmentAttack(equip);
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

  public void refreshHitboxList() {
    hitboxes = new List<GameObject>(lightAttackHitbox); //make a new copy of lgiht attack hitbox list and then add in references to the rest.
    hitboxes.AddRange(heavyAttackHitbox);
    hitboxes.AddRange(equipHitbox);

    pStatManager.hitboxes = hitboxes;
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

  public void equipHeavyAttack(Attack attack) {
    foreach (GameObject hitbox in heavyAttackHitbox) {
      Destroy(hitbox);
    }
    heavyAttack = attack;
    heavyAttackHitbox.AddRange(attack.createHitbox(transform));
    pStatManager.heavyAttack = heavyAttack;
    refreshHitboxList();
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
