using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxLightTwo : HitboxController
{
  public override void updateDamageValue(float newDamage) {
    damage = newDamage * 1.25f;
  }
}
