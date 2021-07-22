using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public class playerStatManager : MonoBehaviour
{

  public List<GameObject> hitboxes = new List<GameObject>();
  public List<Attack> lightAttack;
  public Block block;
  public Attack heavyAttack;
  public Equipable equip;


  public CharacterStat maxHealth = new CharacterStat(100f);
  public CharacterStat baseDamage = new CharacterStat(10f);
  public CharacterStat baseSpeed = new CharacterStat(10f);
  public CharacterStat maxDashes = new CharacterStat(1f);
  public int priority = 0;
  public float currentHealth;
  public GameObject healthBarPrefab;
  public HealthBar healthBar;
  Transform target;
  public PlayerAttacks pa;
  // Start is called before the first frame update
  void Start()
  {
      currentHealth = maxHealth.Value;
      healthBar.SetMaxHealth(maxHealth.Value);
      pa = gameObject.GetComponent<PlayerAttacks>();
      updateDmgValues();
      Invoke("updateDmgValues", 2f);

  }
  private void Awake() {
    target = gameObject.transform;
  }

  void Update(){
    //healthBar.transform.position = new Vector3(target.position.x, target.position.y+2, target.position.z);
  }

  public void updateDmgValues() {
    foreach (GameObject hitbox in hitboxes) {
      hitbox.GetComponent<HitboxController>().updateDamageValue(baseDamage.Value);
    }
  }

  public void TakeDamage(float damage, Vector3 pos) {

    float parryTime = pa.block.returnParryTime();
    Debug.Log(parryTime);
    Vector3 forward = transform.forward;
    Vector3 toOther = pos - transform.position;
    if (pa.blockState == true && (Vector3.Dot(forward.normalized, toOther.normalized) > 0)) {
      currentHealth -= damage * 0.9f;
      Debug.Log("BLOCKED");
    }
    else {
      currentHealth -= damage;
    }
    healthBar.SetHealth(currentHealth/maxHealth.Value);
    if (currentHealth <= 0) {
      //do death stuff here
    }
  }
}
