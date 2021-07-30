using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropTable : MonoBehaviour
{
  public List<GameObject> dropTable = new List<GameObject>();
  public List<int> dropTableAmounts = new List<int>();
  public List<float> dropTableWeights = new List<float>();

    public void DecideDrop()
    {
      float totalWeight = 0f;
      foreach (int weight in dropTableWeights) {
        totalWeight += weight;
      }

      //pick a random number between the weights
      float rarity = Random.Range(0, totalWeight);


      //if the number lands below a weight, then item is chosen from that class.
      int index = 0;
      float decider = 0f;
      foreach (int weight in dropTableWeights) {
        decider += weight;
        if (rarity < decider) {
          Debug.Log(index);
          drop(index);
          return;
        }
        index += 1;
      }
    }

    public void drop(int index) {
      for (int i = 0; i < dropTableAmounts[index]; i++) {
        Instantiate(dropTable[index], transform.position, Quaternion.identity);
      }
    }
}
