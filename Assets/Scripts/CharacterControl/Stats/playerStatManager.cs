using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;
using UnityEngine.SceneManagement;

public class playerStatManager : MonoBehaviour
{
  public int currentMap;
  public bool inShop = false;
  public float scrapAmount = 0;

  public Plane plane = new Plane(Vector3.up, Vector3.zero);
  public bool dashState;
  public float stunTime;
  public bool isParry;
  public float parryWindow;
  public bool isParryStart;
  public bool parried = false;
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
  public bool isHit = false;

  public List<GameObject> hitboxes = new List<GameObject>();
  public List<Attack> lightAttack;
  public Block block;
  public List<Attack>  heavyAttack;
  public Equipable equip;


  public CharacterStat maxNumShields = new CharacterStat(2f);
  public CharacterStat maxHealth = new CharacterStat(100f);
  public CharacterStat baseDamage = new CharacterStat(20f);
  public CharacterStat baseSpeed = new CharacterStat(10f);
  public CharacterStat maxDashes = new CharacterStat(1f);
  public Dictionary<string, CharacterStat> newStats = new Dictionary<string, CharacterStat>();

  public int priority = 0;
  public float currentHealth;
  public int numShields;
  public float numDashes;
  public float dashCooldown;
  public float dashCDTimer;
  public GameObject healthBarPrefab;
  public HealthBar healthBar;
  Transform target;
  public Rigidbody rb;
  public VFXActivate HitVFX;
  public VFXActivate parryVFX;
  // Start is called before the first frame update
  void Start()
  {
      currentHealth = maxHealth.Value;
      numShields = (int)maxNumShields.Value;
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
    if (isParryStart == true) {
      isParryStart = false;
      StartCoroutine("stopParry");
    }
  }

  public void updateDmgValues() {
    foreach (GameObject hitbox in hitboxes) {
      hitbox.GetComponent<HitboxController>().updateDamageValue(baseDamage.Value);
    }
  }

  public void TakeDamage(float damage, GameObject Enemy) {
    if (isHit || dashState == false) return;
    EnemyStatManager ESM = Enemy.GetComponentInChildren<EnemyStatManager>();

    Vector3 forward = transform.forward;
    Vector3 toOther = Enemy.transform.position - transform.position;
    if (blockState == true && (Vector3.Dot(forward.normalized, toOther.normalized) > 0)) {
      if (isParry == true) {
        parryVFX.playVFX();
        parried = true;
        blockTrigger = false;
        ESM.stunTime = stunTime;
        ESM.stunned = true;
        Debug.Log("Parried");
      }
      else {
        currentHealth -= damage * 0.1f;
        isHit = true;
        HitVFX.playVFX();
        Debug.Log("BLOCKED");
        Invoke("notHit", 0.4f);
      }
    }
    else {
      currentHealth -= damage;
      isHit = true;
      HitVFX.playVFX();
      Invoke("notHit", 0.4f);
    }

    if (currentHealth <= 0) {
      if (numShields == 0) {
        die();
      }
      numShields -= 1;
      if (numShields > 0) {
        currentHealth = maxHealth.Value;
      }
    }
    healthBar.SetHealth(currentHealth/maxHealth.Value);
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

  IEnumerator stopParry(){
    yield return new WaitForSeconds(parryWindow);
    isParry = false;
  }

}
