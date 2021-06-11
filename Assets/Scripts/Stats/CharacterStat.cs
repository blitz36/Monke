using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace characterStats {

  [Serializable]
  public class CharacterStat
  {
      public float BaseValue;
      protected float lastBaseValue = float.MinValue;

      protected readonly List<StatModifier> statModifiers;
      public readonly ReadOnlyCollection<StatModifier> StatModifiers;  //public version to show stats
      protected bool isDirty = true;
      protected float _value;
      public readonly ReadOnlyCollection<float> _Value;  //public version to show stats

      public CharacterStat()
      {
          statModifiers = new List<StatModifier>();
          StatModifiers = statModifiers.AsReadOnly();
      }
      //the this() makes it so that it uses this constructor too whenever
      //a base value is called. Required or else an error will occur for statmodifier not being initialized.
      public CharacterStat(float baseValue) : this()
      {
          BaseValue = baseValue;
      }

      // Change Value
      public float Value {
          get {
              if(isDirty || lastBaseValue != BaseValue) {
                  lastBaseValue = BaseValue; //in order to debug and change base value real time, and have it update. otherwise it would only reset on a modified stat.
                  _value = CalculateFinalValue();
                  isDirty = false;
              }
              return _value;
          }
      }

      public virtual void AddModifier(StatModifier mod)
      {
          isDirty = true; //set to dirty and needs to be refreshed whenever value added
          statModifiers.Add(mod);
          statModifiers.Sort(CompareModifierOrder);
      }

      public virtual bool RemoveModifier(StatModifier mod)
      {
          if (statModifiers.Remove(mod))
          {
              isDirty = true;
              return true;
          }
          return false;
      }

      public virtual bool RemoveAllModifiersFromSource(object source)
      {
          bool didRemove = false;

          for (int i = statModifiers.Count - 1; i >= 0; i--)
          {
              if (statModifiers[i].Source == source)
              {
                  isDirty = true;
                  didRemove = true;
                  statModifiers.RemoveAt(i);
              }
          }
          return didRemove;
      }

      //used to make comparisons for modifiers to determine how they should be sorted
      protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
      {
          if (a.Order < b.Order)
              return -1;
          else if (a.Order > b.Order)
              return 1;
          return 0; // if (a.Order == b.Order)
      }

      protected virtual float CalculateFinalValue()
      {
          float finalValue = BaseValue;
          float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers

          for (int i = 0; i < statModifiers.Count; i++)
          {
              StatModifier mod = statModifiers[i];

              if (mod.Type == StatModType.Flat)
              {
                  finalValue += mod.Value;
              }
              else if (mod.Type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
              {
                  sumPercentAdd += mod.Value; // Start adding together all modifiers of this type

                  // If we're at the end of the list OR the next modifer isn't of this type
                  if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                  {
                      finalValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                      sumPercentAdd = 0; // Reset the sum back to 0
                  }
              }
              else if (mod.Type == StatModType.PercentMult)
              {
                  finalValue *= 1 + mod.Value;
              }
          }

          return (float)Math.Round(finalValue, 4);
      }
  }
}
