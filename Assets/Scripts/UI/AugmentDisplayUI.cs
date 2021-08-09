using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AugmentDisplayUI : MonoBehaviour
{
  //public playerStatManager PSM;
  //private Inventory inv;
  public GameObject displayPrefab;
  private Dictionary<string, GameObject> augmentDisplays = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    public void createDisplay(Item item) {
      if (!augmentDisplays.ContainsKey(item.name)) {
        augmentDisplays[item.name] = Instantiate(displayPrefab, transform);
        AugmentDisplayButton button = augmentDisplays[item.name].GetComponent<AugmentDisplayButton>();
        button.stackCount = 1;
        button.updateText(item);
      }
      else {
        AugmentDisplayButton button = augmentDisplays[item.name].GetComponent<AugmentDisplayButton>();
        button.stackCount += 1;
        button.updateText(item);
      }
    }

    public void decrementDisplay(Item item) {
      if (!augmentDisplays.ContainsKey(item.name)) {
        //do some kinda error here. you cant decrement rn theres no item in there
        Debug.Log("UI DISPLAY FAIL, NO ITEM EXIST IN UI");
      }
      else {
        AugmentDisplayButton button = augmentDisplays[item.name].GetComponent<AugmentDisplayButton>();
        button.stackCount -= 1;
        button.updateText(item);
        if (button.stackCount == 0) {
          Destroy(button.gameObject);
        }
      }
    }
}
