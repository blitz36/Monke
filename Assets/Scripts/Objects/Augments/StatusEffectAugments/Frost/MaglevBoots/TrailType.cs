using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailType : MonoBehaviour
{
  public float destroyTime = 3f;
    void Start() {
      Invoke("DestroySelf", destroyTime);
    }

    void DestroySelf() {
      Destroy(gameObject);
    }
}
