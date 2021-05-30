using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
  public GameObject player;
  public float followSpeed = 2.0f;

  void FixedUpdate()
  {
    Vector3 desiredPosition = player.transform.position + new Vector3(-2,18,-10);
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.fixedDeltaTime);
    transform.position = smoothedPosition;

  }
}
