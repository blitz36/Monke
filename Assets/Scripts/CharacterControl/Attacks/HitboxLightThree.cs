using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxLightThree : HitboxController
{
  public override void updateDamageValue(float newDamage) {
    damage = newDamage * 1.5f;
  }
}
