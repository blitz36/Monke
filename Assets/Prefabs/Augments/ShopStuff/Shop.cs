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
  public TMP_Text Price;
  public Image icon;
  public Animator buttonAnim;

  public Item item;
  private float totalWeight = 0f;
  private float decider = 0f;

  public AugmentRarityStorage storage;
  private GameObject rarityList;
  public List<int> Weights;

  private Shop instance;
  private playerStatManager PSM;
  public float price;
  public List<float> priceValuesMin = new List<float>();
  public List<float> priceValuesMax = new List<float>();
  void Awake() {
    PSM = GameObject.FindWithTag("Player").transform.root.GetComponentInChildren<playerStatManager>();
  }

  void Start(){
    instance = this;
    determineLoot();
  }

  public void PickUp() {
    if (PSM.scrapAmount > price) {
      Inventory.instance.Equip(item);
      PSM.scrapAmount -= price;
      buttonAnim.SetBool("EnoughScrap", true);
      buttonAnim.SetBool("Out", true);
      buttonAnim.SetTrigger("Pressed");
    }
    buttonAnim.SetTrigger("Normal");
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
        price = Mathf.Round(Random.Range(priceValuesMin[0], priceValuesMax[0]));
        break;
      case 1:
        itemIndex = Random.Range(0, storage.Rares.Count);
        item = storage.Rares[itemIndex];
        price = Mathf.Round(Random.Range(priceValuesMin[1], priceValuesMax[1]));
        break;
      case 2:
        itemIndex = Random.Range(0, storage.Epics.Count);
        item = storage.Epics[itemIndex];
        price = Mathf.Round(Random.Range(priceValuesMin[2], priceValuesMax[2]));
        break;
      case 3:
        itemIndex = Random.Range(0, storage.Legendary.Count);
        item = storage.Legendary[itemIndex];
        price = Mathf.Round(Random.Range(priceValuesMin[3], priceValuesMax[3]));
        break;
      case 4:
        itemIndex = Random.Range(0, storage.Equips.Count);
        item = storage.Equips[itemIndex];
        price = Mathf.Round(Random.Range(priceValuesMin[4], priceValuesMax[4]));
        break;

    }
    updateText();
  }

  public void updateText() {
    Name.text = item.name;
    Description.text = item.description;
    FlavorText.text = item.FlavorText;
    Price.text = price + " SCRAP";
    icon.sprite = item.icon;
  }

}
