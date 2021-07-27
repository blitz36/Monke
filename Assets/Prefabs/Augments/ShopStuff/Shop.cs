using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
  public TMP_Text Name;
  public TMP_Text Description;
  public TMP_Text FlavorText;
  public Image icon;

  public Item item;
  private float totalWeight = 0f;
  private float decider = 0f;

  public AugmentRarityStorage storage;
  private GameObject rarityList;
  public List<int> Weights;

  private Shop instance;

  void Awake() {
  }

  void Start(){
    instance = this;
    determineLoot();
  }

  public void PickUp() {
      Inventory.instance.Equip(item);
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
        itemIndex = Random.Range(0, storage.Commons.Count);
        item = storage.Commons[itemIndex];
        break;
      case 1:
        itemIndex = Random.Range(0, storage.Rares.Count);
        item = storage.Rares[itemIndex];
        break;
      case 2:
        itemIndex = Random.Range(0, storage.Epics.Count);
        item = storage.Epics[itemIndex];
        break;
      case 3:
        itemIndex = Random.Range(0, storage.Legendary.Count);
        item = storage.Legendary[itemIndex];
        break;
      case 4:
        itemIndex = Random.Range(0, storage.Equips.Count);
        item = storage.Equips[itemIndex];
        break;

    }
    updateText();
  }

  public void updateText() {
    Name.text = item.name;
    Description.text = item.description;
    FlavorText.text = item.FlavorText;
    icon.sprite = item.icon;
  }

}
