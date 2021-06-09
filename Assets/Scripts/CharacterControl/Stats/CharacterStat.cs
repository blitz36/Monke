using System.Collections.Generic;
using System;

public class CharacterStat
{
    public float BaseValue;
    private readonly List<StatModifier> statModifiers;
    private bool isDirty = true;
    private float _value;

    public CharacterStat(float baseValue)
    {
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
    }

    public float Value { //only have to calculate the value in the case that it has been changed
      get {
          if(isDirty) {
              _value = CalculateFinalValue();
              isDirty = false;
          }
          return _value;
      }
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true; //set to dirty and needs to be refreshed whenever value added
        statModifiers.Add(mod);
    }

    public bool RemoveModifier(StatModifier mod)
    {
        isDirty = true;
        return statModifiers.Remove(mod);
    }

    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        //loop to add all the modifiers together
        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.Percent)
            {
                finalValue *= 1 + mod.Value;
            }
        }
        // Rounding gets around dumb float calculation errors (like getting 12.0001f, instead of 12f)
        // 4 significant digits is usually precise enough, but feel free to change this to fit your needs
        return (float)Math.Round(finalValue, 4);
    }
}
