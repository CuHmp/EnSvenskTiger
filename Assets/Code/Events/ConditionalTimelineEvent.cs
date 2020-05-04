using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New ConditionalTimelineEvent", menuName = "GameEvent/ConditionalTimelineEvent", order = 4)]
[System.Serializable]
public class ConditionalTimelineEvent : TimelineEvent
{
    [SerializeField]
    List<TimelineCondition> effect_conditions = new List<TimelineCondition>();
    [SerializeField]
    List<Effect> alternative_effects = new List<Effect>();


    public List<Effect> ChooseEffects()
    {
        bool condition_is_met = false;
        foreach (TimelineCondition condition in effect_conditions)
        {
            condition_is_met = condition.IsMet();
            if (!condition_is_met)
            {
                return alternative_effects;
            }
        }
        return GetEffects();
    }

    [System.Serializable]
    private class TimelineCondition
    {
        [SerializeField]
        string condition;
        [SerializeField]
        variable check_type = variable.INT;
        [SerializeField]
        Resource condition_resource = Resource.Popularity;
        enum variable
        {
            INT,
            BOOL,
            YEAR,
            COUNT,
        }

        public bool IsMet()
        {

            switch (check_type)
            {
                case variable.INT:
                    {
                        int val;
                        Int32.TryParse(condition, out val);
                        if (val >= Player.GetResource(condition_resource))
                        {
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
                        if (myDate == TimeMaster.GetTime())
                        {
                            return true;
                        }
                        return false;
                    }
            }
            return false;
        }
        bool ToBoolean(string value)
        {
            switch (value.ToLower())
            {
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