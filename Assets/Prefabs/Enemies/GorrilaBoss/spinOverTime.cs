using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinOverTime : MonoBehaviour
{
    public float speedOfRotation;
    Quaternion savedRotation;

    void Awake() {
      savedRotation = transform.rotation;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
        transform.rotation = savedRotation;
        transform.Rotate(0,6.0f*speedOfRotation*Time.deltaTime,0);
        savedRotation = transform.rotation;
    //    Transform currentTransform = transform;
      //  currentTransform.Rotate(0,6.0f*speedOfRotation*Time.deltaTime,0);
        //transform.localRotation = currentTransform.rotation;
    }
}
