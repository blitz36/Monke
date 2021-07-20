using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private CinemachineCameraOffset offset;
    public float maxZoom = 5f;
    public float minZoom = -10f;
    // Start is called before the first frame update
    void Awake()
    {
        offset = gameObject.GetComponent<CinemachineCameraOffset>();
    }

    // Update is called once per frame
    void Update()
    {
      if ((Input.mouseScrollDelta.y > 0 && offset.m_Offset.z < maxZoom) || (Input.mouseScrollDelta.y < 0 && offset.m_Offset.z > minZoom))
      offset.m_Offset.z += Input.mouseScrollDelta.y;
      if (offset.m_Offset.z < minZoom) {
        offset.m_Offset.z = minZoom;
      }
      else if (offset.m_Offset.z > maxZoom) {
        offset.m_Offset.z = maxZoom;
      }
    }
}
