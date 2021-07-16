using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXController : MonoBehaviour
{
    public GameObject swordTrail;
    playerStatManager pst;


    void Awake() {
      pst = gameObject.GetComponent<playerStatManager>();
    }
    // Update is called once per frame
    void Update()
    {
      if (pst.priority == 1 || pst.priority == 2) {
        swordTrail.SetActive(true);
      }
      else {
        swordTrail.SetActive(false);
      }
    }
}
