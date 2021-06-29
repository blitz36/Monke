using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MineBehaviour : MonoBehaviour
{   
    [SerializeField] private GameObject toChase;
    [SerializeField] private float alarmRange;
    [SerializeField] private float triggerRange;
    [SerializeField] private LayerMask PlayerLayer;

    private NavMeshAgent myNMAgent;
    private Rigidbody myRigidbody;

    private Transform toChaseTransform;

    private bool playerIsInRange = false;
    private bool activated = false;
    private bool isGrounded = false;
    private bool chaseMode = false;
    private bool isKnockback = false;

    private float knockbackTimer = 1;

    private float accelerateTimer = 2;
    private bool isFullSpeed = false;

    // Signal light
    [SerializeField] private Light signalLight;
    [SerializeField] private float blinkPeriod;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minIntensity;
    [SerializeField] [Range(0f, 1f)] private float intensityPercentage;
    private float currentIntensity;


    private void Start()
    {
        Physics.IgnoreCollision(toChase.GetComponent<Collider>(), GetComponent<Collider>());

        toChaseTransform = toChase.transform;
        myNMAgent = GetComponent<NavMeshAgent>();
        myRigidbody = GetComponent<Rigidbody>();

        myNMAgent.enabled = false;
        myRigidbody.isKinematic = false;
        myRigidbody.useGravity = false;
    }

    private void Update()
    {

        // If player is in range, launch from ground
        //Debug.DrawRay(transform.position, Vector3.down, Color.yellow, .1f);
        if (playerIsInRange && !activated)
        {
            activated = true;
            //Debug.Log("Activated");
            activate();
        }

        // When land on ground, start chasing 
        if (activated && !chaseMode && isGrounded)
        {
            chaseMode = true;
            myNMAgent.enabled = true;
            myRigidbody.isKinematic = true;
        }

        // Chase
        if (chaseMode && !isKnockback)
        {
            chasePlayer();
        }

        // Acceleration timer
        //Debug.Log(accelerateTimer);
        if (chaseMode && accelerateTimer > 0)
        {
            accelerateTimer -= Time.deltaTime;
        } 
        
        if(chaseMode && accelerateTimer <= 0 && !isFullSpeed)
        {
            //myNMAgent.acceleration = 100;
            isFullSpeed = true;
        }

        if (isKnockback && knockbackTimer >= 0)
        {
            knockbackTimer -= Time.deltaTime;
        }


        // Light
        currentIntensity = Mathf.Lerp(minIntensity, maxIntensity, intensityPercentage);
    }

    private void FixedUpdate()
    {
        // Detect player
        if (!activated)
        {
            playerIsInRange = Physics.CheckSphere(transform.position, triggerRange, PlayerLayer);
        }

  
        // Detect if landed
        if (activated && !isGrounded)
        {
            if (myRigidbody.velocity.y < 0)
            {
                isGrounded = Physics.Raycast(transform.position, Vector3.down, .5f);

                //Debug.Log("Grounded: " + isGrounded);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.name == "Hitbox(Clone)" && knockbackTimer <= 0)
        {
            Debug.Log("Weapon hit!");
            if (!isKnockback)
            {
                isKnockback = true;
                myNMAgent.enabled = false;
                myRigidbody.isKinematic = false;
                myRigidbody.useGravity = true;
            }
            knockbackTimer = 1;
            knockback();
        }
    }

    private void activate()
    {
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Launch from ground
        myRigidbody.useGravity = true;
        myRigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        //Debug.Log("Launched");
    }

    private void chasePlayer()
    {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(toChaseTransform.position);
    }

    private void knockback()
    {
        knockbackTimer -= Time.deltaTime;
        Vector3 knockbackDirection = (transform.position - toChase.transform.position).normalized;
        knockbackDirection = new Vector3(knockbackDirection.x, 0, knockbackDirection.z);
        myRigidbody.AddForce(knockbackDirection * 20, ForceMode.Impulse);
        Debug.Log("Knockback force applied!" + knockbackDirection.ToString());
        Debug.DrawLine(transform.position, transform.position + knockbackDirection * 10, Color.red, Mathf.Infinity);
    }

    // Debug
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, triggerRange);
    //}
}