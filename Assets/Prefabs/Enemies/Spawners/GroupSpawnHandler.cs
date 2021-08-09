using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSpawnHandler : MonoBehaviour
{
  public List<EnemySpawner> spawnersList = new List<EnemySpawner>(); //list of synced up spawners
  private bool startSpawn = false; //has trigger started yet
  private int waveCount; //counter of waves
  public int numWaves;// amount of waves to go through
  public List<Vector3> triggerBoxExtants; //size of trigger box
  public List<Vector3> triggerBoxOffsets; //offset of trigger box
  public LayerMask LayerMask;
  public int aliveEnemies;

  public List<GameObject> Walls = new List<GameObject>();

  void OnDrawGizmosSelected()
  {
    int i = 0;
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
        if (hitColliders[j].tag == "Player" && startSpawn == false) {
          startSpawn = true;
        }
      j++;
      }
    }
  }

  void Update() {
    handleWave();

  }

  void handleWaveEnd() {
    foreach (GameObject Wall in Walls) {
      Wall.SetActive(false);
    }
    Destroy(gameObject);
  }

  void handleWave() {
    if (startSpawn) {
      if (aliveEnemies <= 0 && waveCount < numWaves) {
        spawnWave();
        waveCount += 1;
      }
      else if (waveCount == numWaves && aliveEnemies <= 0) {
        handleWaveEnd();
      }
    }
  }

  void spawnWave() {
    foreach (EnemySpawner spawner in spawnersList) {
      aliveEnemies += spawner.spawnWave();
    }
  }

}
