using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounding : MonoBehaviour
{
  public int groundContactCount;
	public bool onGround => groundContactCount > 0;
  float minGroundDotProduct;
  Vector3 contactNormal;
  [SerializeField]
  LayerMask probeMask = -1;
  [SerializeField, Min(0f)]
  float probeDistance = 1f;
  [SerializeField, Range(0f, 90f)]
  float maxGroundAngle = 25f;
  public int stepsSinceLastGrounded;

  Vector3 velocity;
  private Rigidbody rb;

  void Awake () {
		rb = GetComponent<Rigidbody>();
		OnValidate();
	}

  void FixedUpdate() {
    UpdateState();
    rb.velocity = velocity;
    ClearState();
  }

  void OnCollisionEnter (Collision collision) {
    if (collision.gameObject.tag == "Terrain") {
  		EvaluateCollision(collision);
    }
	}

	void OnCollisionStay (Collision collision) {
    if (collision.gameObject.tag == "Terrain") {
  		EvaluateCollision(collision);
    }
	}

  void EvaluateCollision (Collision collision) {
		for (int i = 0; i < collision.contactCount; i++) {
			Vector3 normal = collision.GetContact(i).normal;
      if (normal.y >= minGroundDotProduct) {
        groundContactCount += 1;
        contactNormal += normal;
      }
		}
	}

  void ClearState () {
		groundContactCount = 0;
		contactNormal = Vector3.zero;
	}

  void OnValidate () {
    minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
  }

	void UpdateState () {
  		stepsSinceLastGrounded += 1;
  		velocity = rb.velocity;
  		if (onGround || SnapToGround()) {
  			stepsSinceLastGrounded = 0;
  			if (groundContactCount > 1) {
  				contactNormal.Normalize();
  			}
  		}
  		else {
  			contactNormal = Vector3.up;
  		}
    }

    Vector3 ProjectOnContactPlane (Vector3 vector) {
      return vector - contactNormal * Vector3.Dot(vector, contactNormal);
    }


    bool SnapToGround() {
      if (stepsSinceLastGrounded > 1) {
  			return false;
        //  Debug.Log("1");
  		}
      if (!Physics.Raycast(rb.position, Vector3.down, out RaycastHit hit, probeDistance, probeMask)) {
  			return false;
  		}
  		if (hit.normal.y < minGroundDotProduct) {
  			return false;
  		}

      groundContactCount = 1;
  		contactNormal = hit.normal;
      float speed = velocity.magnitude;
      float dot = Vector3.Dot(velocity, hit.normal);
      if (dot > 0f) {
      //  transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
  //      velocity = (velocity - hit.normal * dot).normalized * speed;

      //  velocity = new Vector3(velocity.x, -98f, velocity.z);

      }
  		return true;
    }
/*
    void AdjustVelocity () {
      Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
  		Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

      float currentX = Vector3.Dot(velocity, xAxis);
      float currentZ = Vector3.Dot(velocity, zAxis);
    }
    */
}
