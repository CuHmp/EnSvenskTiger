using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EventChoice 
{
    public string flavorText;
    public List<Effect> effect;
   
}

public enum Resource
{
    //TODO: Add the resources
    Allies, 
    Axis, 
    Soviets,
    Food,
    Iron,
    Money,
    Popularity
}

[System.Serializable]
public struct Effect
{
    public int change_value;
    public Resource resource;
}
