using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxLightOne : HitboxController
{
    public override void updateDamageValue(float newDamage) {
      damage = newDamage;
    }
}
