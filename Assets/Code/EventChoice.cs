using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EventChoice 
{
    public string flavorText;
    public List<Effect> effect;
}



[System.Serializable]
public class Effect
{
    public int changeValue;
    public Resource resource;
}
