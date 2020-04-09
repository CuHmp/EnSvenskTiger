using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New GenericEvent", menuName = "GameEvent/GenericEvent", order = 0)]
[System.Serializable]
public class GameEvent : ScriptableObject
{
    [SerializeField]
    public string title, flavorText;
    [SerializeField]
    public List<EventChoice> Options;
    [SerializeField]
    public List<Effect> Effects;
    
    public string GetTitle()
    {
        return title;
    }

    public string GetFlavorText()
    {
        return flavorText;
    }

    public List<EventChoice> GetOptions()
    {
        return Options;
    }

    public List<Effect> GetEffects()
    {
        return Effects;
    }


}
[CreateAssetMenu(fileName = "New RandomEvent", menuName = "GameEvent/RandomEvent", order = 1)]
[System.Serializable]
public class RandomEvent : GameEvent
{
    [SerializeField]
    Condition condition;

    public Condition GetConditions()
    {
        return condition;
    }

    [System.Serializable]
    public class Condition
    {
        //TODO: Resource requirements
        [SerializeField]
        UDateTime minDate = (UDateTime)new DateTime(1066, 9, 25), maxDate = (UDateTime)DateTime.Today;

        public bool IsMet()
        {


            if (minDate <= TimeMaster.GetTime() &&
                maxDate >= TimeMaster.GetTime())
            {
                return true;
            }

            return false;
        }
    }
}

[CreateAssetMenu(fileName = "New TimelineEvent", menuName = "GameEvent/TimelineEvent", order = 2)]
[System.Serializable]
public class TimelineEvent : GameEvent
{
    [SerializeField]
    UDateTime date;

    public DateTime GetDate()
    {
        return date;
    }
}



public enum Resource
{
    AlliesOpinion, AlliesPower,
    AxisOpinion, AxisPower,
    SovietsOpinion, SovietPower,
    Food,
    Iron,
    Money,
    Popularity,
    Size
}


