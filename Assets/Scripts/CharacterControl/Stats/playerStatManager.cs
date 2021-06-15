using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public class playerStatManager : MonoBehaviour
{
  public CharacterStat maxHealth = new CharacterStat(100f);
  public float currentHealth;
  public GameObject healthBarPrefab;
  GameObject healthBar;
  Transform target;
  // Start is called before the first frame update
  void Start()
  {
      healthBar = Instantiate(healthBarPrefab);
      healthBar.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
      currentHealth = maxHealth.Value;
      healthBar.GetComponent<HealthBar>().SetMaxHealth(maxHealth.Value);
  }
  private void Awake() {
    target = gameObject.transform;
  }

  void Update(){
    healthBar.transform.position = new Vector3(target.position.x, target.position.y+2, target.position.z);

  }
}
