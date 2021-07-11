using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
  playerStatManager pStatManager;
  public float comboRecoveryTime;
  public float comboBreak;
  public float comboBreak2;
  private int comboStepDB;
  private float timeToBuffer;
  private float timeToBuffer2;
  private float timeToBuffer3;
  public float timeBeforeBuffer;
  private float dontBufferTimer = 0f;

  //equiable items + each attack. Maybe should turn the attacks into a list later?
  public Equipable equip;
  public Attack attack;
  public Attack attack2;
  public Attack attack3;
  public Attack slam;
  public Block block;

  private Rigidbody rb;

  private Plane plane = new Plane(Vector3.up, Vector3.zero);

  public bool bufferAttack; //in order to be able to buffer commands before cooldown is up
  public bool blockState = false; // to check which phase of the block it is in

  //steps in the combo and timers for combos breaking
  public int comboStep = 0;
  public float comboTimer;


  //related to the charge attack and differentiating between tap and hold
  public float holdTimer = 0f;
  public float tapThreshold;
  public float chargePercent;
  private float maxCharge = 1.5f;
  public bool chargeAttack = false;

  //delegate to be able to cancel moves
  public delegate void cancelAttacks();
  public static cancelAttacks cancelAttackFunctions;
  public int oldPriority;

  // Start is called before the first frame update
  void Awake()
  {
    rb = GetComponent<Rigidbody>();
    pStatManager = gameObject.GetComponent<playerStatManager>();
    equip.createHitbox(transform);
    attack.createHitbox(transform);
    attack2.createHitbox(transform);
    attack3.createHitbox(transform);
    slam.createHitbox(transform);
    block.createHitbox(transform);

    if (cancelAttackFunctions == null) {
      cancelAttackFunctions += equip.Cancel;
      cancelAttackFunctions += slam.Cancel;
      cancelAttackFunctions += attack.Cancel;
      cancelAttackFunctions += attack2.Cancel;
      cancelAttackFunctions += attack3.Cancel;
  //    timeToBuffer = attack.totalTime() - timeBeforeBuffer;
  //    timeToBuffer2 = attack2.totalTime() - timeBeforeBuffer;
  //    timeToBuffer3 = attack3.totalTime() - timeBeforeBuffer;
      cancelAttackFunctions += block.Cancel;
    }
  }

  // Update is called once per frame
  void Update()
  {
    timeToBuffer = attack.totalTime() - timeBeforeBuffer;
    timeToBuffer2 = attack2.totalTime() - timeBeforeBuffer;
    timeToBuffer3 = attack3.totalTime() - timeBeforeBuffer;
    if (comboStep != comboStepDB) {
      comboTimer = 0f;
      comboStepDB = comboStep;
    }

    if (pStatManager.priority < 3 || pStatManager.priority == 11){
      block.PerformAttack(rb, plane, gameObject, ref blockState, ref pStatManager.priority, ref comboStep, 0);
    }

    if (pStatManager.priority < 4) {
      equip.Activate(rb, plane, gameObject, ref pStatManager.priority);
    }

    if (pStatManager.priority == 1) {
      dontBufferTimer += Time.deltaTime;
      if (dontBufferTimer > timeToBuffer && comboStep == 0) {
        checkAttacks();
      }
      else if (dontBufferTimer > timeToBuffer2 && comboStep == 1) {
        checkAttacks();
      }
      else if (dontBufferTimer > timeToBuffer3 && comboStep == 2) {
        checkAttacks();
      }
    }
    else {
      dontBufferTimer = 0f;
      checkAttacks();
    }

    if (chargeAttack == true && pStatManager.priority < 3) {
      slam.PerformAttack(rb, plane, gameObject, ref bufferAttack, ref pStatManager.priority, ref comboStep, -2);
    }

    if (pStatManager.priority < 2) {
      //Steps along the combo chain negative means recovery time
      //Case 0: first swing to start combo
      //Case 1: second swing on the combo
      //case 2: last hit
      //case -1 to -3, different recovery phases dependent on which part of the combo you broke out
      switch(comboStep) {
        case 0:
          attack.PerformAttack(rb, plane, gameObject, ref bufferAttack, ref pStatManager.priority, ref comboStep, 1);
        break;

        case 1:
          attack2.PerformAttack(rb, plane, gameObject, ref bufferAttack, ref pStatManager.priority, ref comboStep, 2);
        if (pStatManager.priority != 1) {
          setComboTimer(comboBreak, 0); //how much time before combo breaks
        }
        break;

        case 2:
          attack3.PerformAttack(rb, plane, gameObject, ref bufferAttack, ref pStatManager.priority, ref comboStep, -1);
        if (pStatManager.priority != 1) {
          setComboTimer(comboBreak2, 0); //how much time before combo breaks
        }
        break;

        case -1:
        setComboTimer(comboRecoveryTime, 0);
        break;

        case -2:
        chargeAttack = false;
        setComboTimer(comboRecoveryTime, 0);
        break;
      }
    }

    if (pStatManager.priority != oldPriority) {
      oldPriority = pStatManager.priority;
      if (oldPriority > 1 && pStatManager.priority > 1) {
        switch(pStatManager.priority) {
          case 2:
            cancelAttackFunctions -= slam.Cancel;
            cancelAttackFunctions();
            cancelAttackFunctions += slam.Cancel;
            break;
          case 3:
            cancelAttackFunctions -= equip.Cancel;
          //  cancelAttackFunctions();
            cancelAttackFunctions += equip.Cancel;
            break;
          case 11:
            cancelAttackFunctions -= block.Cancel;
            cancelAttackFunctions();
            cancelAttackFunctions += block.Cancel;
            break;
        }
      }
    }

  }

  //setting a time before moving to the next step in the combo
  void setComboTimer(float maxTime, int NextStep) {
    comboTimer += Time.deltaTime;
    if (comboTimer > maxTime) {
      comboStep = NextStep;
      comboTimer = 0f;
    }
  }

  //Function to buffer attacks and check for charge attacks
  void checkAttacks() {
    if (pStatManager.priority > 1) return;

    if (Input.GetMouseButtonDown(0)) {
      holdTimer = 0f;
    }

    if (Input.GetMouseButton(0)) { //when holding down the mouse, if it passes the threshold then its a charge attack.
      holdTimer += Time.deltaTime;
      if (holdTimer >= tapThreshold) {
        rb.velocity = new Vector3(0, 0, 0);
        if (holdTimer > maxCharge) {
          chargePercent += Time.deltaTime;
          chargeAttack = true;
          bufferAttack = true;
          holdTimer = 0f;
        }
      }
      comboTimer = 0f;
    }
    if (Input.GetMouseButtonUp(0)) {
      if (holdTimer >= tapThreshold) { //do charge attack if tapthreshold is met
        chargeAttack = true;

      }
      bufferAttack = true;
      holdTimer = 0f;
    }
    //  }
  }

}
