using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Make new empty Game Object and put script in with enemy prefab
    public GameObject enemyPrefab;
    public int xPos;
    public int zPos;
    public int xPos2;
    public int zPos2;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        while(enemyCount < 5) // Spawn 5 enemies per spawn area
        {
            xPos = Random.Range(1, 10); // Random X coordinate for spawn area range
            zPos = Random.Range(1, 10); // Random Z coordinate for spawn area range
            Instantiate(enemyPrefab, new Vector3(xPos, 1, zPos), Quaternion.identity); // Spawn enemy at random coordinate
            
            xPos2 = Random.Range(15, 25); // Random X coordinate for spawn area range 2
            zPos2 = Random.Range(1, 10); // Random Z coordinate for spawn area range 2
            Instantiate(enemyPrefab, new Vector3(xPos2, 1, zPos2), Quaternion.identity); // Spawn enemy at random coordinate
            
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }


    }


}
