using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
  public Camera cam;
  public NavMeshAgent agent;

  Rigidbody rb;
  float _speed = 13f;

  void Start ()
  {
      rb = GetComponent<Rigidbody>();
  }

  void Update () {
    float horiz = Input.GetAxisRaw ("Horizontal");
    float vert = Input.GetAxisRaw ("Vertical");
    if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0){
      Vector3 fVelocity = new Vector3(horiz, 0, vert).normalized * _speed;
      rb.velocity = fVelocity;
    }
    else {
      rb.velocity = new Vector3(0,0,0);
    }
  }
}
