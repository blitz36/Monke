using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
  Transform target;

    void Awake()
    {
    target = Camera.main.transform;
    }

    void LateUpdate()
    {
      transform.LookAt(transform.position + target.transform.rotation * Vector3.back, target.transform.rotation * Vector3.up);
    }
}
