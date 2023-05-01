using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat 
{
    [SerializeField]
    private float baseValue;

    private List<float> modifiers = new List<float>();
    public float GetValue()
    {

        float value = 0f;
        foreach (float mod in modifiers)
        {
            value += mod;
        }
        return baseValue+value;
    }
    // Start is called before the first frame update
   public void AddModifier( float modifier)
    {
        if(modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }
    public void RemoveModifier (float modifier)
    {
        if(modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
