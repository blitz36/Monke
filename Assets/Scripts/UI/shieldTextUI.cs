using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shieldTextUI : MonoBehaviour
{
  public TMP_Text maxShieldText;
  public TMP_Text currentShieldText;
  public TMP_Text totalShieldText;
  public playerStatManager PSM;

    void Awake()
    {
      if (PSM == null){
        PSM = transform.root.GetComponentInChildren<playerStatManager>();
      }
    }

    // Update is called once per frame
    void Update()
    {
      maxShieldText.text = PSM.maxHealth.Value.ToString();
      currentShieldText.text = PSM.currentHealth.ToString();
      if (PSM.numShields > 0) {
        totalShieldText.text = ((PSM.numShields - 1)*PSM.maxHealth.Value).ToString();
      }
      else {
        totalShieldText.text = "0";
      }
    }
}
