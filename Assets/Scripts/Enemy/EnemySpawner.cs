using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemySpawner : MonoBehaviour
{
    // Make new empty Game Object and put script in with enemy prefab
    public bool randomSpawn = true;
    public bool startSpawn = false;
    public LayerMask LayerMask;
    public GameObject enemyPrefab;
    public List<Vector3> spawnBoxExtants;
    public List<Vector3> triggerBoxExtants;
    public List<Vector3> spawnBoxOffsets;
    public List<Vector3> triggerBoxOffsets;
    public List<GameObject> Gates;

    private int waveCount;
    public int maxWaves;
    public int numSpawnsMin;
    public int numSpawnsMax;
    private int numSpawns;
    public float timeBetweenWaves;

    void Awake() {
      numSpawns = Random.Range(numSpawnsMin, numSpawnsMax);

    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        int i = 0;
        foreach (Vector3 extant in spawnBoxExtants) {
          Gizmos.color = new Color(1, 0, 0, 0.5f);
          Gizmos.DrawCube(transform.position + spawnBoxOffsets[i], extant*2);
          i++;
        }
        // Draw a semitransparent blue cube at the transforms position
        i = 0;
        foreach (Vector3 extant in triggerBoxExtants) {
          Gizmos.color = new Color(0, 0, 1, 0.5f);
          Gizmos.DrawCube(transform.position + triggerBoxOffsets[i], extant*2);
          i++;
        }
    }

    void FixedUpdate() {
      if (startSpawn == true) return;

      int i = 0;
      foreach (Vector3 extant in triggerBoxExtants) {
        Collider[] hitColliders = Physics.OverlapBox(transform.position + triggerBoxOffsets[i], extant, Quaternion.identity, LayerMask);
        i++;
        int j = 0;
        //Check when there is a new collider coming into contact with the box
        while (j < hitColliders.Length)
        {
          Debug.Log("Hit : " + hitColliders[j].name + j);
          if (hitColliders[j].tag == "Player" && startSpawn == false) {
            startSpawn = true;
            StartCoroutine(spawnEnemy());
          }
        j++;
        }
      }

    }

    IEnumerator spawnEnemy()
    {
      while(waveCount < maxWaves)
        {
        if (randomSpawn == true) {
                numSpawns = Random.Range(numSpawnsMin, numSpawnsMax);
                for (int i = 0; i < numSpawns; i++) {

                  int j = (int )Random.Range(0, spawnBoxExtants.Count);
                  float xPos = Random.Range(spawnBoxExtants[j].x*-1, spawnBoxExtants[j].x); // Random X coordinate for spawn area range
                  float zPos = Random.Range(spawnBoxExtants[j].z*-1, spawnBoxExtants[j].z); // Random Z coordinate for spawn area range
                  xPos += spawnBoxOffsets[j].x;
                  zPos += spawnBoxOffsets[j].z;
                  Instantiate(enemyPrefab, new Vector3(transform.position.x+xPos, 1, transform.position.z+zPos), Quaternion.identity); // Spawn enemy at random coordinate
            }
        }
        else {
              for (int i = 0; i < spawnBoxExtants.Count; i++) {
                float xPos = Random.Range(spawnBoxExtants[i].x*-1, spawnBoxExtants[i].x); // Random X coordinate for spawn area range
                float zPos = Random.Range(spawnBoxExtants[i].z*-1, spawnBoxExtants[i].z); // Random Z coordinate for spawn area range
                xPos += spawnBoxOffsets[i].x;
                zPos += spawnBoxOffsets[i].z;
                Instantiate(enemyPrefab, new Vector3(transform.position.x+xPos, 1, transform.position.z+zPos), Quaternion.identity); // Spawn enemy at random coordinate
            }
        }
        yield return new WaitForSeconds(timeBetweenWaves);
        waveCount += 1;
      }
    }
}
