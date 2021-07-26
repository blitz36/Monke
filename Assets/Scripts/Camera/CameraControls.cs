using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private playerStatManager PSM;
    private CinemachineCameraOffset offset;
    public float maxZoom = 5f;
    public float minZoom = -10f;
    // Start is called before the first frame update
    void Awake()
    {
        offset = gameObject.GetComponent<CinemachineCameraOffset>();
        PSM = transform.root.GetComponentInChildren<playerStatManager>();
    }

    // Update is called once per frame
    void Update()
    {
      if ((PSM.playerInput.Base.Zoom.ReadValue<float>() > 0 && offset.m_Offset.z < maxZoom) || (PSM.playerInput.Base.Zoom.ReadValue<float>() < 0 && offset.m_Offset.z > minZoom))
      offset.m_Offset.z += PSM.playerInput.Base.Zoom.ReadValue<float>();
      if (offset.m_Offset.z < minZoom) {
        offset.m_Offset.z = minZoom;
      }
      else if (offset.m_Offset.z > maxZoom) {
        offset.m_Offset.z = maxZoom;
      }
    }
}
