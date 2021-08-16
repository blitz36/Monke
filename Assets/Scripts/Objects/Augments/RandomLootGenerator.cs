using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomLootGenerator : MonoBehaviour
{
  public Text itemDescription;

  public GameObject common;
  public GameObject rare;
  public GameObject epic;
  public GameObject legendary;

  private GameObject chest;

  public GameObject rarityList;
  public AugmentRarityStorage storage;

  public List<Item> Commons;
  public List<Item> Rares;
  public List<Item> Epics;
  public List<Item> Legendary;
  public List<Item> Equips;
  public List<int> Weights;
  public int totalWeight;
  private int decider = 0;
  public Item item;


  private RandomLootGenerator instance;

  void Awake() {
    storage = rarityList.GetComponent<AugmentRarityStorage>();
    syncStoredItems();
  }

  void Start() {
    instance = this;

    determineLoot();
    itemDescription.gameObject.SetActive(false);
  }
/*
  void OnTriggerEnter(Collider col) {
    itemDescription.gameObject.SetActive(true);
  }

  void OnTriggerExit(Collider col) {
    itemDescription.gameObject.SetActive(false);
  }

  void OnTriggerStay(Collider Collider) {
    if (Input.GetKey(KeyCode.E)) {
      PickUp(instance);
    }
  }
*/

  public void syncStoredItems() {
    //Commons = storage.Commons;
//    Rares = storage.Rares;
    Epics = storage.Epics;
    Legendary = storage.Legendary;
    Equips = storage.Equips;
  }

  public void PickUp() {
      Inventory.instance.Equip(item);
      Destroy(gameObject);
  }


  public void determineLoot() {
    //add up all the weights
    foreach (int weight in Weights) {
      totalWeight += weight;
    }

    //pick a random number between the weights
    float rarity = Random.Range(0, totalWeight);


    //if the number lands below a weight, then item is chosen from that class.
    int index = 0;
    foreach (int weight in Weights) {
      decider += weight;
      if (rarity < decider) {
        chooseFromRarity(index);
        return;
      }
      index += 1;
    }
  }

  public void chooseFromRarity(int index) {
    int itemIndex;
    switch(index) {
      case 0:
        itemIndex = Random.Range(0, Commons.Count);
        item = Commons[itemIndex];
        chest = Instantiate(common, gameObject.transform, false);
        break;
      case 1:
        itemIndex = Random.Range(0, Rares.Count);
        item = Rares[itemIndex];
        chest = Instantiate(rare, gameObject.transform, false);
        break;
      case 2:
        itemIndex = Random.Range(0, Epics.Count);
        item = Epics[itemIndex];
        chest = Instantiate(epic, gameObject.transform, false);
        break;
      case 3:
        itemIndex = Random.Range(0, Legendary.Count);
        item = Legendary[itemIndex];
        chest = Instantiate(legendary,gameObject.transform, false);
        break;
      case 4:
        itemIndex = Random.Range(0, Equips.Count);
        item = Equips[itemIndex];
        break;

    }
    itemDescription.text = item.description;
  }
}
