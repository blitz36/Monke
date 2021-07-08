using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
  playerStatManager pStatManager;

  //equiable items + each attack. Maybe should turn the attacks into a list later?
  public Equipable equip;
  public Attack attack;
  public Attack slam;
  public Attack block;

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
  private float maxCharge = 1f;
  public bool chargeAttack = false;

  //delegate to be able to cancel moves
  public delegate void cancelAttacks();
  public static cancelAttacks cancelAttackFunctions;
  public int oldPriority;

  // Start is called before the first frame update
  void Awake()
  {
    equip.createHitbox(transform);
    attack.createHitbox(transform);
    slam.createHitbox(transform);
    block.createHitbox(transform);
    cancelAttackFunctions += equip.Cancel;
    cancelAttackFunctions += slam.Cancel;
    cancelAttackFunctions += attack.Cancel;
  //  cancelAttackFunctions += block.Cancel;
    rb = GetComponent<Rigidbody>();
    pStatManager = gameObject.GetComponent<playerStatManager>();
  }

  // Update is called once per frame
  void Update()
  {
    if (pStatManager.priority < 1 || pStatManager.priority == 11){
      block.PerformAttack(rb, plane, gameObject, ref blockState, ref pStatManager.priority, ref comboStep, -3);
    }

    if (pStatManager.priority < 4) {
      equip.Activate(rb, plane, gameObject, ref pStatManager.priority);
    }

    checkAttacks();

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
          attack.PerformAttack(rb, plane, gameObject, ref bufferAttack, ref pStatManager.priority, ref comboStep, 2);
        setComboTimer(2f, 0); //how much time before combo breaks
        break;

        case 2:
          attack.PerformAttack(rb, plane, gameObject, ref bufferAttack, ref pStatManager.priority, ref comboStep, -1);
        setComboTimer(2f, 0); //how much time before combo breaks
        break;

        case -1:
        if (Input.GetMouseButtonUp(0)) {
          bufferAttack = true;
        }
        setComboTimer(1f, 0);
        break;

        case -2:
        setComboTimer(0.3f, 0);
        chargeAttack = false;
        if (Input.GetMouseButtonUp(0)) {
          bufferAttack = true;
        }
        break;

        case -3:
        setComboTimer(0.3f, 0);
        if (Input.GetMouseButtonUp(0)) {
          bufferAttack = true;
        }
        break;
        case -4:
        setComboTimer(0.4f, 0);
        if (Input.GetMouseButtonUp(0)) {
          bufferAttack = true;
        }
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
            cancelAttackFunctions();
            cancelAttackFunctions += equip.Cancel;
            break;
          case 10:
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

    if (Input.GetMouseButton(0)) { //when holding down the mouse, if it passes the threshold then its a charge attack.
      holdTimer += Time.deltaTime;
      if (holdTimer >= tapThreshold) {
        PlayerMovement.isAction = true;
        rb.velocity = new Vector3(0, 0, 0);
        if (holdTimer > maxCharge) {
          chargePercent += Time.deltaTime;
          chargeAttack = true;
          bufferAttack = true;
          holdTimer = 0f;
        }
      }
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
