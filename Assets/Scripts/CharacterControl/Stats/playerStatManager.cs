using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public class playerStatManager : MonoBehaviour
{
  public List<GameObject> hitboxes = new List<GameObject>();

  public CharacterStat maxHealth = new CharacterStat(100f);
  public CharacterStat baseDamage = new CharacterStat(10f);
  public CharacterStat baseSpeed = new CharacterStat(10f);
  public CharacterStat maxDashes = new CharacterStat(1f);
  public int priority = 0;
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
      updateDmgValues();
      Invoke("updateDmgValues", 2f);

  }
  private void Awake() {
    target = gameObject.transform;
  }

  void Update(){
    healthBar.transform.position = new Vector3(target.position.x, target.position.y+2, target.position.z);

  }

  public void updateDmgValues() {
    foreach (GameObject hitbox in hitboxes) {
      hitbox.GetComponent<HitboxController>().updateDamageValue(baseDamage.Value);
    }
  }

  public void TakeDamage(float damage) {
    currentHealth -= damage;
    healthBar.GetComponent<HealthBar>().SetHealth(currentHealth);
    if (currentHealth <= 0) {
      //do death stuff here
    }
  }
}
