using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;
using UnityEngine.SceneManagement;

public class playerStatManager : MonoBehaviour
{

  private bool isHit = false;

  public List<GameObject> hitboxes = new List<GameObject>();
  public List<Attack> lightAttack;
  public Block block;
  public Attack heavyAttack;
  public Equipable equip;


  public CharacterStat maxHealth = new CharacterStat(100f);
  public CharacterStat baseDamage = new CharacterStat(20f);
  public CharacterStat baseSpeed = new CharacterStat(10f);
  public CharacterStat maxDashes = new CharacterStat(1f);
  public CharacterStat lifeSteal = new CharacterStat(0f);
  public int priority = 0;
  public float currentHealth;
  public GameObject healthBarPrefab;
  public HealthBar healthBar;
  Transform target;
  public PlayerAttacks pa;
  public PlayerMovement pm;
  // Start is called before the first frame update
  void Start()
  {
      currentHealth = maxHealth.Value;
      healthBar.SetMaxHealth(maxHealth.Value);
      pa = gameObject.GetComponent<PlayerAttacks>();
      pm = gameObject.GetComponent<PlayerMovement>();
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
    if (isHit == true) return;

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
    isHit = true;
    Invoke("notHit", 0.2f);
    if (currentHealth <= 0) {
      die();
    }
  }

  public void notHit() {
    isHit = false;
  }

  public void die(){
    SceneManager.LoadScene(0);
    Time.timeScale=1f;
    GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
    GameObject[] objs2 = GameObject.FindGameObjectsWithTag("MainCamera");
    Destroy(objs[0]);
    Destroy(objs[0].transform.parent.gameObject);
    Destroy(objs2[0]);
  }
}
