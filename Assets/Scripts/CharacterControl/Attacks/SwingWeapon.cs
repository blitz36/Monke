using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingWeapon : MonoBehaviour
{
  public GameObject weapon;
  public  float rotationSpeed = 2f;
  public float cooldownTime = 1f;
  public float cooldownTimer;
  private int swingState = 0;

  private Quaternion savedRotation;
  private Vector3 savedPosition;
  private Vector3 currentAngle;
    // Start is called before the first frame update
    void Start()
    {
      foreach (Transform child in transform)
      {
          if(child.tag == "Weapon")
          {
              weapon = child.gameObject;
              savedRotation = weapon.transform.rotation;
              savedPosition = weapon.transform.localPosition;
          }
      }
    }

    // Update is called once per frame
    void Update()
    {
      switch (swingState) {
        case 0: //available to use
          var isSlashKeyDown = Input.GetKeyDown(KeyCode.Mouse0);
          if(isSlashKeyDown)
            {
              swingState = 1;
              weapon.transform.localRotation = Quaternion.Euler(90f,90f,0f);
              cooldownTimer = 0;
          }
          break;
        case 1: //In motion
          weapon.transform.RotateAround(this.transform.position, transform.up, Time.deltaTime * -360);
          cooldownTimer += Time.deltaTime * 3;
          if(cooldownTimer >= cooldownTime)
          {
              cooldownTimer = cooldownTime;
              weapon.transform.rotation = savedRotation;
              weapon.transform.localPosition = savedPosition;
              swingState = 0;
          }
          break;
        case 2: //Cooldown

          break;
      }

      if (Input.GetKeyDown (KeyCode.Mouse0)) {
         transform.Rotate(0,50*Time.deltaTime, 0);
      }
    }

}
