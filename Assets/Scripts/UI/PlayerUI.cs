using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text fpsDisplay;
    public Text dashDisplay;
    public Text equipDisplay;
    public float deltaTime;
    private playerStatManager PSM;

    void Awake(){
      PSM = transform.root.GetComponentInChildren<playerStatManager>();
    }

    // Update is called once per frame
    void Update()
    {
      deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
      float fps = 1.0f / deltaTime;
      fpsDisplay.text = Mathf.Ceil (fps).ToString ();
    }
}
