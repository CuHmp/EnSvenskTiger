using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New GenericEvent", menuName = "GameEvent/GenericEvent", order = 0)]
[System.Serializable]
public class GameEvent : ScriptableObject
{
    [SerializeField]
    protected LangString title, flavorText;
    [SerializeField]
    protected EventChoice[] Options = new EventChoice[2];
    [SerializeField]
    protected List<Effect> Effects;
    
    public string GetTitle()
    {
        return title.GetText();
    }
    public LangString GetLangStringTitle() {
        return title;
    }
    public LangString GetLangStringFlavorText() {
        return flavorText;
    }
    public string GetFlavorText()
    {
        return flavorText.GetText();
    }

    public EventChoice[] GetOptions()
    {
        return Options;
    }

    public List<Effect> GetEffects()
    {
        return Effects;
    }
    
    public void setEvent(GameEvent e, List<Effect> effect) {
        title = e.GetLangStringTitle();
        flavorText = e.GetLangStringFlavorText();
        Options = e.GetOptions();
        Effects = effect;
    }

}

[CreateAssetMenu(fileName = "New RandomEvent", menuName = "GameEvent/RandomEvent", order = 1)]
[System.Serializable]
public class RandomEvent : GameEvent
{
    [SerializeField]
    Condition condition;

    public Condition GetCondition()
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

[CreateAssetMenu(fileName = "New ResourceEvent", menuName = "GameEvent/ResourceEvent", order = 3)]
[System.Serializable]
public class ResourceEvent : GameEvent
{

    
}

[CreateAssetMenu(fileName = "New ConditionalTimelineEvent", menuName = "GameEvent/ConditionalTimelineEvent", order = 4)]
[System.Serializable]
public class ConditionalTimelineEvent : TimelineEvent {
    [SerializeField]
    List<TimelineCondition> effect_conditions = new List<TimelineCondition>();
    [SerializeField]
    List<Effect> alternative_effects = new List<Effect>();


    public List<Effect> ChooseEffects() {
        bool condition_is_met = false;
        foreach(TimelineCondition tc in effect_conditions) {
            condition_is_met = tc.IsMet();
            if (!condition_is_met) {
                return alternative_effects;
            }
        }
        return GetEffects();
    }
   
    [System.Serializable]
    private class TimelineCondition {
        [SerializeField]
        string condition;
        [SerializeField]
        variable check_type = variable.INT;
        [SerializeField]
        Resource condition_resource = Resource.Popularity;
        enum variable {
            INT,
            BOOL,
            YEAR,
            COUNT,
        }

        public bool IsMet() {

            switch (check_type) {
                case variable.INT: 
                {
                    int val;
                    Int32.TryParse(condition, out val);
                    if (val >= Player.GetResource(condition_resource)) {
                        return true;
                    }
                    return false;
                }
                case variable.BOOL: 
                {
                    bool val = ToBoolean(condition);
                    return val;
                }
                case variable.YEAR: 
                {
                    DateTime myDate = DateTime.Parse(condition);
                    if (myDate == TimeMaster.GetTime()) {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
        bool ToBoolean(string value) {
            switch (value.ToLower()) {
                case "true":
                    return true;
                case "t":
                    return true;
                case "1":
                    return true;
                case "0":
                    return false;
                case "false":
                    return false;
                case "f":
                    return false;
                default:
                    return false;
            }
        }
    }

}


public enum Resource
{
    AlliesOpinion, AlliesPower,
    AxisOpinion, AxisPower,
    SovietOpinion, SovietPower,
    Food,
    Iron,
    Money,
    Popularity,
    Count
}