using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    public float detectionRange;
    public float timeBetweenChecks;

    private Transform nearestTarget;
    private EnemyStatManager ESM;
    private IEnumerator check;
    // Start is called before the first frame update
    void Awake()
    {
      if (ESM == null) {
        ESM = gameObject.transform.root.GetComponent<EnemyStatManager>();
      }
    }

    // Update is called once per frame
    void Start()
    {
      check = RoutineTargetCheck();
      StartCoroutine(check);
    }

    void Update() {
      if (nearestTarget == null) {
        StopCoroutine(check);
        StartCoroutine(check);
      }
    }

    IEnumerator RoutineTargetCheck() {
      while (true) {
        CheckNearestTarget();
        yield return new WaitForSeconds(timeBetweenChecks);
      }

    }

    void CheckNearestTarget() {
      Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, targetLayer);
      float minimumDistance = Mathf.Infinity;
      foreach(Collider collider in hitColliders)
      {
          float distance = Vector3.SqrMagnitude(collider.transform.position - transform.position);
          if (distance < minimumDistance)
          {
              minimumDistance = distance;
              nearestTarget = collider.transform;
          }
      }
      ESM.target = nearestTarget;
    }
}
