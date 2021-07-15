using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
  public float seconds;
    void Start() {
      Invoke("DestroyGM", seconds);
    }

    void DestroyGM() {
      Destroy(gameObject);
    }
}
