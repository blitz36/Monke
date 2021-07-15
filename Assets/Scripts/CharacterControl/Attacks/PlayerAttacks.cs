using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
  playerStatManager pst;
  public float comboRecoveryTime;
  public float comboBreak;
  public float comboBreak2;
  private int comboStepDB;
  private float timeToBuffer;
  private float timeToBuffer2;
  private float timeToBuffer3;
  public float timeBeforeBuffer;
  private float dontBufferTimer = 0f;

  //equiable items + each lightAttack. Maybe should turn the lightAttacks into a list later?
  public Equipable equip;
  public List<Attack> lightAttack;
  public Attack heavyAttack;
  public Block block;

  private Rigidbody rb;

  private Plane plane = new Plane(Vector3.up, Vector3.zero);

  public bool bufferAttack; //in order to be able to buffer commands before cooldown is up
  public bool blockState = false; // to check which phase of the block it is in
  private Vector3 faceDirection;

  //steps in the combo and timers for combos breaking
  public int comboStep = 0;
  public float comboTimer;


  //related to the charge lightAttack and differentiating between tap and hold
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
    pst = gameObject.GetComponent<playerStatManager>();
    block.createHitbox(transform);

    if (cancelAttackFunctions == null) {
      cancelAttackFunctions += equip.Cancel;
      cancelAttackFunctions += heavyAttack.Cancel;
      cancelAttackFunctions += lightAttack[0].Cancel;
      cancelAttackFunctions += lightAttack[1].Cancel;
      cancelAttackFunctions += lightAttack[2].Cancel;
  //    timeToBuffer = lightAttack.totalTime() - timeBeforeBuffer;
  //    timeToBuffer2 = lightAttack2.totalTime() - timeBeforeBuffer;
  //    timeToBuffer3 = lightAttack3.totalTime() - timeBeforeBuffer;
      cancelAttackFunctions += block.Cancel;
    }
  }

  void Start() {
    refreshEquips();
  }

  // Update is called once per frame
  void Update()
  {
    timeToBuffer = lightAttack[0].totalTime() - timeBeforeBuffer;
    timeToBuffer2 = lightAttack[1].totalTime() - timeBeforeBuffer;
    timeToBuffer3 = lightAttack[2].totalTime() - timeBeforeBuffer;

    //IF ITS A NEW COMBO STEP, THEN RESET THE COMBO TIMER BACK TO 0 THAT WAY COMBO TIMER DOESNT LEAK OVER
    if (comboStep != comboStepDB) {
      comboTimer = 0f;
      comboStepDB = comboStep;
    }

    //BLOCK LOGIC
    if (pst.priority < 3 || pst.priority == 11){
      block.PerformAttack(rb, plane, gameObject, ref blockState, ref pst.priority, ref comboStep, 0);
    }

    //EQUIP LOGIC
    if (pst.priority < 4) {
      equip.Activate(rb, plane, gameObject, ref pst.priority);
    }

    //LOGIC ON WHEN TO BE ABLE TO QUEUE UP NEW LIGHT AND HEAVY ATTACKS
    if (pst.priority == 1) {
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


    //HEAVY ATTACK LOGIC
    if (chargeAttack == true && pst.priority < 3) {
      heavyAttack.PerformAttack(rb, plane, gameObject, ref bufferAttack, ref pst.priority, ref comboStep, -2);
    }


    //LIGHT ATTACK LOGIC
    if (pst.priority < 2) {

      if (comboStep >= 0) {
        int newComboStep = 0;
        if (comboStep > lightAttack.Count-2) {
          newComboStep = -1;
        }
        else {
          newComboStep = comboStep + 1;
        }

        lightAttack[comboStep].PerformAttack(rb, plane, gameObject, ref bufferAttack, ref pst.priority, ref comboStep, newComboStep);
      }

      if (pst.priority != 1 && comboStep > 0) {
        setComboTimer(comboBreak, 0); //how much time before combo breaks
      }
      else if (comboStep < 0) {
          setComboTimer(comboRecoveryTime, 0);
          chargeAttack = false;
        }
    }

    if (pst.priority != oldPriority) {
      holdTimer = 0f;
      oldPriority = pst.priority;
      if (oldPriority > 1 && pst.priority > 1) {
        switch(pst.priority) {
          case 2:
            cancelAttackFunctions -= heavyAttack.Cancel;
            cancelAttackFunctions();
            cancelAttackFunctions += heavyAttack.Cancel;
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

  //Function to buffer lightAttacks and check for charge lightAttacks
  void checkAttacks() {
    if (pst.priority > 1) return;

    if (Input.GetMouseButtonDown(0)) {
      holdTimer = 0f;
    }

    if (Input.GetMouseButton(0)) { //when holding down the mouse, if it passes the threshold then its a charge lightAttack.
      holdTimer += Time.deltaTime;
      if (holdTimer >= tapThreshold) {
        gameObject.transform.eulerAngles = faceDirection;
        rb.velocity = new Vector3(0, 0, 0);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter;
        if (plane.Raycast(ray, out enter))
        {
            var hitPoint = ray.GetPoint(enter);
            var mouseDir = hitPoint - gameObject.transform.position;
            mouseDir = mouseDir.normalized;
            gameObject.transform.LookAt (hitPoint);
            faceDirection = new Vector3(0, gameObject.transform.eulerAngles.y,0);
            }
        /*
        if (holdTimer > maxCharge) {
          chargePercent += Time.deltaTime;
          chargeAttack = true;
          bufferAttack = true;
          holdTimer = 0f;
        }
        */
      }
      comboTimer = 0f;
    }
    if (Input.GetMouseButtonUp(0)) {
      if (holdTimer >= tapThreshold) { //do charge lightAttack if tapthreshold is met
        chargeAttack = true;

      }
      bufferAttack = true;
      holdTimer = 0f;
    }
    //  }
  }

  public void refreshEquips() {
    lightAttack = pst.lightAttack;
    block = pst.block;
    heavyAttack = pst.heavyAttack;
    equip = pst.equip;
  }

}
