using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMarker : MonoBehaviour
{
  public Transform Player;
  public Transform targetPosition;
  public RectTransform arrowTransform;

  [SerializeField]
  private GameObject pointer;




    // Update is called once per frame
    void Update()
    {
      if (targetPosition == null) return;
        Vector3 toPosition = targetPosition.transform.position;
        Vector3 fromPosition = Player.position;
        fromPosition.y = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        arrowTransform.rotation = Quaternion.LookRotation(dir);
    }

    void OnTriggerEnter(Collider collider) {
      if (collider.tag == "ObjectiveReset") {
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(collider.gameObject.transform.root.gameObject);
      }
    }
}
