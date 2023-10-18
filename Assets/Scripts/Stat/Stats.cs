using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]   
public class Stats
{
    [SerializeField]private int baseValue;

    public List<int> modifiers;

    public int GetValue()
    {
        int finalValue = baseValue;
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue ;
    }

    public virtual void SetDefaultValue(int modifier)
    {
        baseValue = modifier;
    }
    public virtual void AddModifier(int modifier)
    {
        modifiers.Add(modifier);
    }

    public virtual void RemoveModifier(int modifier)
    {
        modifiers.Remove(modifier);
    }
}
