using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingWeapon : MonoBehaviour
{
  public GameObject hitbox;
  private Renderer hitboxRenderer;
  public static GameObject weapon;
  private Rigidbody rb;
  public int clickForce = 500;
  private Plane plane = new Plane(Vector3.up, Vector3.zero);
  //timers needed to perform a swing
  public float accel;
  public float decel;
  public float startUpTime;
  private float startUpTimer;
  public float cooldownTime = 0.1f;
  public static float cooldownTimer = 0f;
  private float swingTime = 0.05f;
  private float swingTimer;
  private int swingState = 0;

  //to reset position to what it was before
  public static Quaternion savedRotationSW;
  public static Vector3 savedPositionSW;

  //steps in the combo
  public int comboStep = 0;
  public float comboTimer;
  private Vector3 StartRot; //starting rotation angle for hitbox
    // Start is called before the first frame update
    void Start()
    {
      hitboxRenderer = hitbox.GetComponent<Renderer>();
      rb = GetComponent<Rigidbody>();
      //finding the weapon to manipulate
      foreach (Transform child in transform)
      {
          if(child.tag == "Weapon")
          {
              weapon = child.gameObject;
              savedRotationSW = weapon.transform.rotation;
              savedPositionSW = weapon.transform.localPosition;
          }
      }
    }

    // Update is called once per frame
    void Update()
    {

      switch(comboStep) {
        case 0:
          StartRot = new Vector3(90f, 90f, 0f);
          performSwing(StartRot, -3600, 1, 0.05f);
          break;

        case 1:
          comboTimer += Time.deltaTime;
          if (comboTimer > 0.5f) {
            comboStep = -1;
            comboTimer = 0f;
            break;
          }
          StartRot = new Vector3(90f, -90f, 0f);
          performSwing(StartRot, 3600, 2, 0.05f);
          break;

        case 2:
          comboTimer += Time.deltaTime;
          if (comboTimer > 0.5f) {
            comboStep = -2;
            comboTimer = 0f;
            break;
          }
          StartRot = new Vector3(45f, 90f, 0f);
          performSwing(StartRot, -3600, -3, 0.05f);
          break;


        case -1:
          comboTimer += Time.deltaTime;
          if (comboTimer > 0.3f) {
            comboStep = 0;
          }
          break;

        case -2:
          comboTimer += Time.deltaTime;
          if (comboTimer > 0.4f) {
            comboStep = 0;
          }
          break;

        case -3:
          comboTimer += Time.deltaTime;
          if (comboTimer > 0.4f) {
            comboStep = 0;
          }
          break;
      }
    }

    void performSwing(Vector3 StartRotation, int RotationSpeed, int NextStep, float activeTime) {
      switch (swingState) {
        case 0: //Starting state,
          hitboxRenderer.material.SetColor("_Color", Color.white);
          var isSlashKeyDown = Input.GetButton("Fire1");
          if(isSlashKeyDown)
            {
              //dashing in the direction of the mouse code\
              rb.velocity = new Vector3(0, 0, 0);
              var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

              float enter;
              if (plane.Raycast(ray, out enter))
              {
                  var hitPoint = ray.GetPoint(enter);
                  var mouseDir = hitPoint - gameObject.transform.position;
                  mouseDir = mouseDir.normalized;
                  rb.AddForce(mouseDir * clickForce);

              //setting variables for next steps + rotating weapon to attack position
              PlayerMovement.isAction = true;
              swingState = 1;
              weapon.transform.localRotation = Quaternion.Euler(StartRotation.x, StartRotation.y, StartRotation.z);
              swingTimer = 0;
              startUpTimer = 0;
          }
        }
        break;

        case 1: //start up
        rb.velocity = rb.velocity * decel;
        hitboxRenderer.material.SetColor("_Color", Color.red);
          startUpTimer += Time.deltaTime;
          if (startUpTimer >= startUpTime) {
            swingState = 2;
          }
          break;
        case 2: //Active
          rb.velocity = new Vector3(0f,0f,0f);
          hitboxRenderer.material.SetColor("_Color", Color.green);
          weapon.transform.RotateAround(this.transform.position, transform.up, Time.deltaTime * RotationSpeed);
          swingTimer += Time.deltaTime;
          if(swingTimer >= activeTime)
          {
              swingTimer = swingTime;
              cooldownTimer = 0f;
              swingState = -1;

          }
          break;
        case -1: //recovery
          hitboxRenderer.material.SetColor("_Color", Color.blue);
          cooldownTimer += Time.deltaTime;
          if (cooldownTimer >= cooldownTime) {
            weapon.transform.rotation = savedRotationSW;
            weapon.transform.localPosition = savedPositionSW;
            comboStep = NextStep;
            PlayerMovement.isAction = false;
            comboTimer = 0f; //in reference to the combo attack system
            swingState = 0;
          }
          break;
      }
    }


}
