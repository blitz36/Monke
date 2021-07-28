using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapZoom : MonoBehaviour
{
  public CameraControls CC;
  private playerStatManager PSM;
  public float minZoom;
  public float maxZoom;
  public Camera m_OrthographicCamera;

  void Awake() {
    PSM = transform.root.GetComponentInChildren<playerStatManager>();
  }

    // Update is called once per frame
    void Update()
    {

      if (CC.onUI == true) {
        if ((PSM.playerInput.Base.Zoom.ReadValue<float>() > 0 && m_OrthographicCamera.orthographicSize <= maxZoom) || (PSM.playerInput.Base.Zoom.ReadValue<float>() < 0 && m_OrthographicCamera.orthographicSize >= minZoom))
        m_OrthographicCamera.orthographicSize -= PSM.playerInput.Base.Zoom.ReadValue<float>()/90f;
        if (m_OrthographicCamera.orthographicSize < minZoom) {
          m_OrthographicCamera.orthographicSize = minZoom;
        }
        else if (m_OrthographicCamera.orthographicSize > maxZoom) {
          m_OrthographicCamera.orthographicSize = maxZoom;
        }
      }
    }
}
