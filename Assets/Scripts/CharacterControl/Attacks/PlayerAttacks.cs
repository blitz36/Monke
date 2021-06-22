using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
  //hit box related things, will not be as needed when animations are put in.
  private Renderer hitboxRenderer; //to change color for visual testing
  private Renderer hitboxRenderer2; //to change color for visual testing
  public static GameObject weapon; //this is how we will get reference to the hitbox
  private Rigidbody rb;
  public GameObject firstHitbox;
  public GameObject chargeHitbox;
  public GameObject blockHitbox;

  //relating towards momentum from smacking
  public int clickForce = 500;
  private Plane plane = new Plane(Vector3.up, Vector3.zero);
  //celleration for when smacking and momentum
  public float decel;

  //timers needed to perform a swing
  private float startUpTimer;

  private float swingTimer;

  public float cooldownTime; //cooldown between each swing
  public static float cooldownTimer = 0f;


  private int swingState = 0; //swing state to determine which part of the swing we are at
  public bool bufferAttack; //in order to be able to buffer commands before cooldown is up


//block related timers
  public float blockCooldownTimer;
  public float blockStartUpTimer;
  public static int blockState = 0; // to check which phase of the block it is in



  //to reset position to what it was before
  public static Quaternion savedRotationSW;
  public static Vector3 savedPositionSW;

  //steps in the combo and timers for combos breaking
  public int comboStep = 0;
  public float comboTimer;


  //related to the charge attack and differentiating between tap and hold
  public float holdTimer = 0f;
  public float tapThreshold;
  public float chargePercent;
  private float maxCharge = 1f;
  public bool chargeAttack = false;
  private bool chargeCancel = false;

    // Start is called before the first frame update
    void Start()
    {
      firstHitbox.SetActive(false);
      chargeHitbox.SetActive(false);
      hitboxRenderer = firstHitbox.transform.GetChild(1).GetComponent<Renderer>();
      hitboxRenderer2 = chargeHitbox.GetComponent<Renderer>();
      rb = GetComponent<Rigidbody>();
      //finding the weapon to manipulate the rotation
      foreach (Transform child in transform)
      {
          if(child.tag == "Weapon")
          {
              weapon = child.gameObject;
          }
      }
    }

    // Update is called once per frame
    void Update()
    {
      //cant dash and swing
      if (PlayerMovement.dashState != 1) {

        //Steps along the combo chain negative means recovery time
        //Case 0: first swing to start combo
        //Case 1: second swing on the combo
        //case 2: last hit
        //case -1 to -3, different recovery phases dependent on which part of the combo you broke out
        switch(comboStep) {
          case 0:
            if (swingState == 0) {
              performBlock(0.1166f, 0.2f);
            }
            checkAttacks();
            if (blockState == 0){
              performSwing(1, 0.1166f, 0.05f, 0.2f); //run through the different attack states
            }
            break;

          case 1:
            if (swingState == 0) {
              performBlock(0.1166f, 0.2f);
            }
            checkAttacks();
            if (blockState == 0){

              performSwing(2, 0.1166f, 0.05f, 0.2f);
            }
            setComboTimer(0.5f, 0); //how much time before combo breaks
            break;

          case 2:
            if (swingState == 0) {
              performBlock(0.1166f, 0.2f);
            }
            checkAttacks();
            if (blockState == 0){
              performSwing(-3, 0.1166f, 0.05f, 0.2f);
            }
            setComboTimer(0.5f, 0); //how much time before combo breaks
            break;

          case -1:
            if (Input.GetMouseButtonUp(0)) {
              bufferAttack = true;
            }
            setComboTimer(0.3f, 0);
            break;

          case -2:
            setComboTimer(0.4f, 0);
            if (Input.GetMouseButtonUp(0)) {
              bufferAttack = true;
            }
            break;

          case -3:
            setComboTimer(0.4f, 0);
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

    }

    //setting a time before moving to the next step in the combo
    void setComboTimer(float maxTime, int NextStep) {
      if (swingState == 0){ //combo should only be able to break if you arent already doing a combo
        comboTimer += Time.deltaTime;
          if (comboTimer > maxTime) {
            comboStep = NextStep;
            comboTimer = 0f;
          }
        }
    }

    void checkAttacks() {
    //  if (!swingState == 1) { //only need to check for inputs whenever it is in the idle phase
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

    //steps along the hitting process
    //Case 0: idle state check for inputs to start the slash
    //Case 1: start up animation and while accelerating in direction of the hit
    //Case 2: Active frames where it is slashing, will check for hitbox here.
    //Case -1: Recovery time before being able to move again.
    void performSwing(int NextStep, float startUpTime, float activeTime, float recoveryTime) {
      switch (swingState) {
        case 0: //Starting/idle state


          if(bufferAttack) //if slashing or a slash is buffered then perform the action
            {
              //dashing in the direction of the mouse for some momentum. raycast to a floor, then add force ein that direction
              rb.velocity = new Vector3(0, 0, 0);
              var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              float enter;
              if (plane.Raycast(ray, out enter))
              {
                  var hitPoint = ray.GetPoint(enter);
                  var mouseDir = hitPoint - gameObject.transform.position;
                  mouseDir = mouseDir.normalized;
                  rb.AddForce(mouseDir * clickForce);
                  transform.LookAt (hitPoint);
                  transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);

              //setting variables for next steps + rotating weapon to attack position
              PlayerMovement.isAction = true;
              if (chargeAttack == false) {
                firstHitbox.SetActive(true);
              }
              else {
                chargeHitbox.SetActive(true);
              }
              swingState = 1;
              startUpTimer = 0;
              bufferAttack = false;
          }
        }
        break;

        case 1: //start up
        hitboxRenderer.material.SetColor("_Color", Color.red);
        hitboxRenderer2.material.SetColor("_Color", Color.red);
        //decelerate the momentum during startup
        rb.velocity = rb.velocity * decel;

        //timer to switch to active frames
          startUpTimer += Time.deltaTime;
          if (startUpTimer >= startUpTime) {
            swingTimer = 0;
            swingState = 2;
          }
          break;

        case 2: //Active
          hitboxRenderer.material.SetColor("_Color", Color.green);
          hitboxRenderer2.material.SetColor("_Color", Color.green);
          //stop all momentum at this point
          rb.velocity = new Vector3(0f,0f,0f);


          //timer before switching to recovery stage
          swingTimer += Time.deltaTime;
          if(swingTimer >= activeTime)
          {
              swingTimer = 0f;
              cooldownTimer = 0f;
              swingState = -1;

          }
          break;

        case -1: //recovery
          hitboxRenderer.material.SetColor("_Color", Color.blue);
          hitboxRenderer2.material.SetColor("_Color", Color.magenta);

          if (Input.GetMouseButton(0)) {
            holdTimer += Time.deltaTime;
            if (holdTimer >= tapThreshold) {
              hitboxRenderer.material.SetColor("_Color", Color.magenta);
              if (holdTimer > maxCharge) {
                chargeAttack = true;
                bufferAttack = true;
                swingState = 0;
                comboStep = 0;
                holdTimer = 0f;
              }
            }
          }
          if (Input.GetMouseButtonUp(0)) {
            if (holdTimer >= tapThreshold) { //do charge attack if tapthreshold is met
              chargeAttack = true;
              swingState = 0;
              comboStep = 0;
              bufferAttack = true;
              holdTimer = 0f;
            }
          }




          //timer to reset to the next combostep and reset the transforms
          cooldownTimer += Time.deltaTime;
          if (cooldownTimer >= recoveryTime || chargeCancel == true) {
            if (chargeAttack == false) {
              firstHitbox.SetActive(false);
            }
            else {
              chargeHitbox.SetActive(false);
            }
            comboStep = NextStep;
            PlayerMovement.isAction = false; //let them MOVE AGAIN
            chargeAttack = false;
            holdTimer = 0f;
            comboTimer = 0f; //in reference to the combo attack system
            swingState = 0;
            hitboxRenderer.material.SetColor("_Color", Color.white);
          }
          break;
      }
    }

    void performBlock(float startupTime, float recoveryTime) {
      switch (blockState) {
        case 0: //Starting/idle state
          if(Input.GetMouseButton(1)) //gotta be able to buffer blocks while in recovery
            {
              //no moving until block is over
              rb.velocity = new Vector3(0, 0, 0);
              var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              float enter;
              if (plane.Raycast(ray, out enter))
              {
                  var hitPoint = ray.GetPoint(enter);
                  var mouseDir = hitPoint - gameObject.transform.position;
                  mouseDir = mouseDir.normalized;
                  transform.LookAt (hitPoint);
                  transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
                //  rb.AddForce(mouseDir * clickForce);

              //setting variables for next steps + rotating weapon to attack position
              PlayerMovement.isAction = true;

              blockState = 1;
              blockStartUpTimer = 0;
          }
        }
        break;

        case 1: //start up
        //timer to switch to active frames
          blockStartUpTimer += Time.deltaTime;
          if (blockStartUpTimer >= startupTime) {
            blockState = 2;
            blockHitbox.SetActive(true);
          }
          break;

        case 2: //Active
          if(!Input.GetMouseButton(1))
          {
              blockCooldownTimer = 0f;
              blockState = -1;
              blockHitbox.SetActive(false);

          }
          break;

        case -1: //recovery

          //timer to reset to the next combostep and reset the transforms
          blockCooldownTimer += Time.deltaTime;
          if (blockCooldownTimer >= recoveryTime) {
            comboStep = 0;
            PlayerMovement.isAction = false; //let them MOVE AGAIN
            comboTimer = 0f; //in reference to the combo attack system
            blockState = 0;
          }
          break;
      }
    }
}
