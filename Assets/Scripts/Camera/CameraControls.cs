using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraControls : MonoBehaviour
{
    public bool onUI = false;

    private playerStatManager PSM;
    public CinemachineTransposer transposer;
    private CinemachineCameraOffset offset;
    public CinemachineVirtualCamera cam;
    public float maxZoom = 5f;
    public float minZoom = -10f;
    // Start is called before the first frame update
    void Awake()
    {
        if (cam == null) {
          cam = gameObject.GetComponent<CinemachineVirtualCamera>();
        }
        transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        offset = gameObject.GetComponent<CinemachineCameraOffset>();
        PSM = transform.root.GetComponentInChildren<playerStatManager>();
    }

    // Update is called once per frame
    void Update()
    {
      onUICheck();

      if (PSM.playerInput.Base.RotateCamLeft.ReadValue<float>() > 0) {
        rotateAroundPoint(-1f);
      }
      if (PSM.playerInput.Base.RotateCamRight.ReadValue<float>() > 0) {
        rotateAroundPoint(1f);
      }

      if (onUI) return;
      zoom();
    }

    void rotateAroundPoint(float angle) {
      angle = (angle) * (Mathf.PI/180); // Convert to radians
      var rotatedX = Mathf.Cos(angle) * (transposer.m_FollowOffset.x) - Mathf.Sin(angle) * (transposer.m_FollowOffset.z);
      var rotatedZ = Mathf.Sin(angle) * (transposer.m_FollowOffset.x) + Mathf.Cos(angle) * (transposer.m_FollowOffset.z);
    //  transposer.m_FollowOffset = new Vector3(Mathf.Lerp(transposer.m_FollowOffset.x, rotatedX, Time.deltaTime * 1), transposer.m_FollowOffset.y, Mathf.Lerp(transposer.m_FollowOffset.z, rotatedZ, Time.deltaTime * 1));
      transposer.m_FollowOffset.z = rotatedZ;
      transposer.m_FollowOffset.x = rotatedX;
    }

    void zoom() {
      if ((PSM.playerInput.Base.Zoom.ReadValue<float>() > 0 && offset.m_Offset.z < maxZoom) || (PSM.playerInput.Base.Zoom.ReadValue<float>() < 0 && offset.m_Offset.z > minZoom))
      offset.m_Offset.z += PSM.playerInput.Base.Zoom.ReadValue<float>()/120f;
      if (offset.m_Offset.z < minZoom) {
        offset.m_Offset.z = minZoom;
      }
      else if (offset.m_Offset.z > maxZoom) {
        offset.m_Offset.z = maxZoom;
      }
    }

    public void onUICheck() {
    //  if (EventSystem.current.IsPointerOverGameObject()) {
        PointerEventData PointerEventData = new PointerEventData(EventSystem.current);
        PointerEventData.position = Input.mousePosition;

        List<RaycastResult> RaycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(PointerEventData, RaycastResultList);

        onUI = false;
        for (int i = 0; i < RaycastResultList.Count; i++) {
          if (RaycastResultList[i].gameObject.GetComponent<MiniMapZoom>() != null) {
            //do zoomin logic here
            onUI = true;
          }
        }
    //  }
    }

}
