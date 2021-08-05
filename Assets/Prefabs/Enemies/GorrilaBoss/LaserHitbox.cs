using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitbox : MonoBehaviour
{
  public float damage;
  public float laserRange = 30f;
  private LaserLineRender laserRender;
  public LayerMask layerMask;
  public float radius;
  void Awake(){
    laserRender = gameObject.GetComponent<LaserLineRender>();
    radius = laserRender.width;
  }

  void Update() {
    RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, laserRange, layerMask))
    //    if (Physics.SphereCast(transform.position, radius, transform.TransformDirection(Vector3.forward), out hit, laserRange, layerMask))
        {
            if (hit.transform.tag == "Player")
            {
              playerStatManager pst = hit.transform.GetComponent<playerStatManager>();
              pst.TakeDamage(damage, transform.root.gameObject);
            }
            laserRender.setLaser(transform.position, hit.point);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
          Vector3 laserMaxPosition = transform.position+transform.forward*laserRange;
          laserRender.setLaser(transform.position, laserMaxPosition);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
  }
}
