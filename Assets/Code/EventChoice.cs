﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EventChoice 
{
    [SerializeField]
    string flavorText;
    [SerializeField]
    List<Effect> effect;

    public string GetFlavorText()
    {
        return flavorText;
    }

    public List<Effect> GetEffects()
    {
        return effect;
    }

    public Effect GetEffectAtIndex(int index)
    {
        if (index < GetEffectsCount())
        {
            return effect[index];
        }
        return new Effect();
    }

    public int GetEffectsCount()
    {
        return effect.Count;
    }


}



[System.Serializable]
public class Effect
{
    [SerializeField]
    int changeValue;
    [SerializeField]
    Resource resource;

    public int GetValue()
    {
        return changeValue;
    }

    public Resource GetResource()
    {
        return resource;
    }

}
