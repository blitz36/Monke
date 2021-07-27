using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;
using UnityEngine.SceneManagement;

public class playerStatManager : MonoBehaviour
{
  public Plane plane = new Plane(Vector3.up, Vector3.zero);
  public bool isRunning;
  public bool blockState;
  public bool blockTrigger;
  public bool bufferedAttack = false;
  public bool chargeAttack = false;
  public int chargeAttackType;
  public float holdTimer;
  public List<float> holdTimes;
  public bool holding;
  public float tapThreshold;
  public int comboStep = 0;

  public Inputs playerInput;
  private bool isHit = false;

  public List<GameObject> hitboxes = new List<GameObject>();
  public List<Attack> lightAttack;
  public Block block;
  public List<Attack>  heavyAttack;
  public Equipable equip;


  public CharacterStat maxHealth = new CharacterStat(100f);
  public CharacterStat baseDamage = new CharacterStat(20f);
  public CharacterStat baseSpeed = new CharacterStat(10f);
  public CharacterStat maxDashes = new CharacterStat(1f);
  public Dictionary<string, CharacterStat> newStats = new Dictionary<string, CharacterStat>();

  public int priority = 0;
  public float currentHealth;
  public float numDashes;
  public float dashCooldown;
  public float dashCDTimer;
  public GameObject healthBarPrefab;
  public HealthBar healthBar;
  Transform target;
  public Rigidbody rb;
  // Start is called before the first frame update
  void Start()
  {
      currentHealth = maxHealth.Value;
      healthBar.SetMaxHealth(maxHealth.Value);
      numDashes = maxDashes.Value;
      updateDmgValues();
      Invoke("updateDmgValues", 2f);

      playerInput.Base.HeavyAttack.started += _ => holding = true;
      playerInput.Base.HeavyAttack.performed += _ => holding = false;
      playerInput.Base.HeavyAttack.canceled += _ => holding = false;

      playerInput.Base.Block.started += _ => blockTrigger = true;
      playerInput.Base.Block.performed += _ => blockTrigger = false;
      playerInput.Base.Block.canceled += _ => blockTrigger = false;

  }
  private void Awake() {
    target = gameObject.transform;
    playerInput = new Inputs();
    if (rb == null) {
      rb = gameObject.GetComponent<Rigidbody>();
    }
  }

  private void OnEnable() {
      playerInput.Enable();
  }

  private void OnDisable() {
    playerInput.Disable();
  }

  void Update(){
    dashRefresh();
    holdInput();
  }

  public void updateDmgValues() {
    foreach (GameObject hitbox in hitboxes) {
      hitbox.GetComponent<HitboxController>().updateDamageValue(baseDamage.Value);
    }
  }

  public void TakeDamage(float damage, Vector3 pos) {
    if (isHit == true) return;

  //  float parryTime = pa.block.returnParryTime();
    Vector3 forward = transform.forward;
    Vector3 toOther = pos - transform.position;
    if (blockState == true && (Vector3.Dot(forward.normalized, toOther.normalized) > 0)) {
      currentHealth -= damage * 0.1f;
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

  void dashRefresh() {
    if (numDashes >= maxDashes.Value) {
      return;
    }

    dashCDTimer += Time.deltaTime;
    if(dashCDTimer >= dashCooldown) //when recovery time is up reset everything so u can dash again :)
    {
        dashCDTimer = 0;
        numDashes += 1;
    }
  }

  void holdInput(){
    if (holding == false) {
      if (holdTimer > 0f) {
        if (holdTimer < holdTimes[0]) {
          chargeAttackType = 0;
        }
        else if (holdTimer < holdTimes[1]) {
          chargeAttackType = 1;
        }
        else {
          chargeAttackType = 2;
        }
        holdTimer = 0f;
      }
      return;
    }
    else {
      holdTimer += Time.deltaTime;
    }
  }

}
