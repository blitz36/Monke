using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using characterStats;

public class playerStatManager : MonoBehaviour
{
  //animation handling
  public int priority = 0;

  //handles shop and economy stuff
  public bool inShop = false;
  public float scrapAmount = 0;
  public int currentMap;

  public Plane plane = new Plane(Vector3.up, Vector3.zero);

  //Relating to the dash and movement in general
  public bool dashState;
  public float numDashes;
  public float dashCooldown;
  public float dashCDTimer;
  public bool isRunning;

  //blocking and parryu related variables
  public bool isParry;
  public float parryWindow;
  public bool isParryStart;
  public bool parried = false;

  public bool blockState;
  public bool blockTrigger;

  //light and heavy attack related variables
  public int comboStep = 0;
  public bool bufferedAttack = false;
  public bool chargeAttack = false;
  public int chargeAttackType;
  public float holdTimer;
  public List<float> holdTimes;
  public bool holding;
  public float tapThreshold;

  //every type of hitbox for gameobjects, and every attack type to go with it
  public List<GameObject> hitboxes = new List<GameObject>();
  public List<GameObject> lightAttackHitboxes = new List<GameObject>();
  public List<GameObject> heavyAttackHitboxes = new List<GameObject>();
  public List<GameObject> equipHitboxes = new List<GameObject>();
  public List<Attack> lightAttack;
  public Block block;
  public List<Attack>  heavyAttack;
  public Equipable equip;

  //Every form of trackable stats with base stats, and included stats from augments
  public CharacterStat maxNumShields = new CharacterStat(2f);
  public CharacterStat maxHealth = new CharacterStat(100f);
  public CharacterStat baseDamage = new CharacterStat(20f);
  public CharacterStat baseSpeed = new CharacterStat(10f);
  public CharacterStat maxDashes = new CharacterStat(1f);
  public CharacterStat critChancePerc = new CharacterStat(.1f);
  public CharacterStat healthRegenValue = new CharacterStat(1f);
  public Dictionary<string, CharacterStat> newStats = new Dictionary<string, CharacterStat>();
  public Dictionary<string, GameObject> augmentHitboxes = new Dictionary<string, GameObject>();

  //health related stuff
  private PlayerHealthHandler PHH;
  public float currentHealth;
  public int numShields;
  public GameObject healthBarPrefab;
  public HealthBar healthBar;
  public Rigidbody rb;
  public bool isHit = false;
  public float stunTime;

  //Vfx and effects
  public VFXActivate HitVFX;
  public VFXActivate parryVFX;
  public bool stoppingTime = false;
  public Inputs playerInput;
  public SideInputManager SPM;
  // Start is called before the first frame update
  void Start()
  {
    SPM = gameObject.GetComponent<SideInputManager>();
    playerInput = SPM.playerInput;
      currentHealth = maxHealth.Value;
      numShields = (int)maxNumShields.Value;
      healthBar.SetMaxHealth(maxHealth.Value);
      numDashes = maxDashes.Value;
      updateDmgValues();
      Invoke("updateDmgValues", 2f);
      StartCoroutine(PHH.baseHealOverTime(this));
  }

  private void Awake() {
    if (rb == null) {
      rb = gameObject.GetComponent<Rigidbody>();
    }
    if (PHH == null) {
      PHH = gameObject.GetComponent<PlayerHealthHandler>();
    }
  }

  void Update(){
    dashRefresh();
    if (isParryStart == true) { //timer to start the countdown to turn off parry window
      isParryStart = false;
      StartCoroutine("stopParry");
    }
  }

  public void updateDmgValues() {
    foreach (GameObject hitbox in hitboxes) {
      hitbox.GetComponent<HitboxController>().updateDamageValue(baseDamage.Value);
    }
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

  IEnumerator stopParry(){
    yield return new WaitForSeconds(parryWindow);
    isParry = false;
  }

  public void StopTime(float timeToResume, float timeToResumeSlow) {
    StartCoroutine(Stop(timeToResume, timeToResumeSlow));
  }
  private IEnumerator Stop(float timeToResume, float timeToResumeSlow) {

    if ((timeToResume <= 0 && timeToResumeSlow <= 0) || stoppingTime) {
      yield break;
    }
    if (!stoppingTime) {
      stoppingTime = true;
      Time.timeScale = 0f;
      yield return new WaitForSecondsRealtime(timeToResume);
      Time.timeScale = 0.1f;
      yield return new WaitForSecondsRealtime(timeToResumeSlow);
      Time.timeScale = 1f;
      stoppingTime = false;
    }
  }

  public void healDamage(float heal) {
    PHH.healDamage(heal, this);
  }

  public void TakeDamage(float damage, GameObject Enemy) {
    PHH.TakeDamage(damage, Enemy, this);
  }


}
