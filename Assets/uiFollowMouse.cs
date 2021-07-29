using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiFollowMouse : MonoBehaviour
{
    public RectTransform MovingObject;
    public Vector3 offset;
    public RectTransform CanvasObject;
    public Canvas m_canvas;

    public void Update()
    {
        //MoveObject();
        
        //Vector3 vector = new Vector3(Input.mousePosition.x, InputmousePosition.y, Camera.main.nearClipPlane.ClipPlane);
        //Debug.Log(Camera.main.ScreenToWorldPoint(vector));
    }

    //public void MoveObject()
    //{
        //MovingObject.anchoredPosition = Input.mousePosition;
        
        //Vector2 anchoredPos;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasObject, Input.mousePosition, m_canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : cam, out anchoredPos);
        //MovingObject.anchoredPosition = anchoredPos;
    //}
}
