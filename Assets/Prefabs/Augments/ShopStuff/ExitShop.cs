using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitShop : MonoBehaviour
{
  private playerStatManager PSM;
    // Start is called before the first frame update
    void Awake()
    {
      if (PSM == null){
        PSM = GameObject.FindWithTag("Player").transform.root.GetComponentInChildren<playerStatManager>();
      }
    }

    // Update is called once per frame
    public void leaveShop()
    {
      PSM.currentMap = PSM.currentMap + 1;
      PSM.inShop = false;
      SceneManager.LoadScene(PSM.currentMap);
    }
}
