using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public class EnemyStatManager : MonoBehaviour
{
    public float timeToResume;
    public Rigidbody rb;

    public CharacterStat maxHealth = new CharacterStat(90f);
    public float currentHealth;

    public GameObject healthBarPrefab;
    GameObject healthBar;
    Transform target;

    float timer = 0f;
    public float speed;

    public bool isHit = false;

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
      rb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = new Vector3(target.position.x, target.position.y+2, target.position.z);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        isHit = true;
        Invoke("notHit", 0.15f);
        Time.timeScale = 0.05f;
        Invoke("resumeTime", timeToResume);
        healthBar.GetComponent<HealthBar>().SetHealth(currentHealth);

        if (currentHealth < 0f) {
          Destroy(healthBar);
          Destroy(gameObject);
        }
    }
    void raiseMax(float value) {
        maxHealth.AddModifier(new StatModifier(value, StatModType.PercentAdd, this));
        currentHealth = maxHealth.Value;
    }
    public void notHit() {
      isHit = false;
    }
    public void resumeTime(){
      Time.timeScale = 1f;
    }
}
