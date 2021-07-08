using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public class EnemyStatManager : MonoBehaviour
{
    public CharacterStat maxHealth = new CharacterStat(100f);
    public float currentHealth;

    public GameObject healthBarPrefab;
    GameObject healthBar;
    Transform target;

    float timer = 0f;
    public float speed;
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
    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = new Vector3(target.position.x, target.position.y+2, target.position.z);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.GetComponent<HealthBar>().SetHealth(currentHealth);
    }
    void raiseMax(float value) {
        maxHealth.AddModifier(new StatModifier(value, StatModType.PercentAdd, this));
        currentHealth = maxHealth.Value;
    }
}
