using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnersCluster
{
    public bool syncTrigger;
    public bool syncLocation;
    public bool syncMaxWaves;
    public int maxWaves;

    public List<GameObject> Spawner;

    public void syncCluster(){
      foreach (GameObject spawnObject in Spawner) {
          EnemySpawner spawner = spawnObject.GetComponent<EnemySpawner>();
          if (syncTrigger == true) {

          }
          if (syncLocation == true) {

          }
          if (syncMaxWaves == true) {
            spawner.maxWaves = maxWaves;
          }
      }
    }

}

[System.Serializable]
public class SpawnerTotals
{
    public List<SpawnersCluster> Clusters;
}

public class SpawnerManager : MonoBehaviour
{
  public SpawnerTotals clusterList;

  void Start() {
    foreach (SpawnersCluster cluster in clusterList.Clusters) {
      cluster.syncCluster();
    }
  }

}
