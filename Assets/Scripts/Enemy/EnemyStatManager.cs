using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public class EnemyStatManager : MonoBehaviour
{
    public GroupSpawnHandler spawnHandler;
    public int currentAnim;

    [HideInInspector]
    public StateController SC;
    public float dissolveTime;
    public float hitStunTime;
    public float stunTime;
    public bool stunned;

    [HideInInspector]
    public Rigidbody rb;

    public CharacterStat maxHealth = new CharacterStat(90f);
    public float currentHealth;
    public CharacterStat baseSpeed = new CharacterStat(5f);
    public GameObject healthBarPrefab;
    public HealthBar healthBar;
    Transform healthBarTarget;
    public Transform target;

    public bool isHit;

    public GameObject vfx;
    public EnemyDropTable EDT;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth.Value;
        healthBar.SetMaxHealth(maxHealth.Value);
    }
    private void Awake() {
      target = GameObject.FindWithTag("Player").transform;

      healthBarTarget = gameObject.transform;
      rb = gameObject.GetComponent<Rigidbody>();
      EDT = gameObject.GetComponent<EnemyDropTable>();

      if (SC == null) {
        SC = gameObject.GetComponent<StateController>();
      }
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
      if (damage > 9f) {
        isHit = true;
      }
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth/maxHealth.Value);
        vfx.GetComponent<VFXActivate>().playVFX();

    }
    void raiseMax(float value) {
        maxHealth.AddModifier(new StatModifier(value, StatModType.PercentAdd, this));
        currentHealth = maxHealth.Value;
    }

  public void notHit() {
    isHit = false;
  }

  public void destroySelf() {
    spawnHandler.aliveEnemies -= 1;
    Destroy(gameObject);
  }
}
