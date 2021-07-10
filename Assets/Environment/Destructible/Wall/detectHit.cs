using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectHit : MonoBehaviour
{
    [SerializeField] private GameObject destroyed;
    [SerializeField] private float breakForce;
    private bool hasHit = false;

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (!hasHit)
        {
            //Debug.Log("hit");
            hasHit = true;
            GameObject newDestroyedInstance = Instantiate(destroyed, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
