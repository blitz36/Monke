using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLineRender : MonoBehaviour
{
  private LineRenderer laser;
  public float width;
  public float length;
    void Awake()
    {
        laser = GetComponent<LineRenderer>();
        laser.SetWidth(width, length);
    }


    public void setLaser(Vector3 startPoint, Vector3 endPoint) {
      laser.SetPosition(0, startPoint);
      laser.SetPosition(1, endPoint);
    }
}
