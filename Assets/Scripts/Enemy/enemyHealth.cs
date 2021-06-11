using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public class enemyHealth : MonoBehaviour
{
    public CharacterStat maxHealth = new CharacterStat(100f);
    public float currentHealth;
    public GameObject healthBarPrefab;
    GameObject healthBar;
    Transform target;
    int direction = 1;
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
        switch (direction){
          case 1:
              timer += Time.deltaTime;
              if (timer > 1f) {
                timer = 0;
                direction = 2;
              }
              transform.Translate(Vector3.forward * Time.deltaTime * speed);
              break;
          case 2:
            timer += Time.deltaTime;
            if (timer > 1f) {
              timer = 0;
              direction = 3;
            }
            transform.Translate(Vector3.right * Time.deltaTime* speed);
            break;
          case 3:
            timer += Time.deltaTime;
            if (timer > 1f) {
              timer = 0;
              direction = 4;
            }
            transform.Translate(Vector3.forward * -Time.deltaTime* speed);
            break;
          case 4:
            timer += Time.deltaTime;
            if (timer > 1f) {
              timer = 0;
              direction = 1;
            }
            transform.Translate(Vector3.right * -Time.deltaTime* speed);
            break;

        }

        healthBar.transform.position = new Vector3(target.position.x, target.position.y+2, target.position.z);
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            raiseMax(1f);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.GetComponent<HealthBar>().SetHealth(currentHealth);
    }
    void raiseMax(float value) {
        maxHealth.AddModifier(new StatModifier(value, StatModType.PercentAdd, this));
        currentHealth = maxHealth.Value;
    }
}
