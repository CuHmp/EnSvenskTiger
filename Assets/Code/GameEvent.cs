using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Event", menuName = "GameEvent/Event", order = 1)]
[ExecuteInEditMode]
public class GameEvent : ScriptableObject
{
    public string title, flavorText;

    public List<EventChoice> Options;
    public List<Condition> Conditions; 
    

    public class Condition
    {
        Resource resource;
        Compare compare;
        int value;
        DateTime date;

        public bool IsMet()
        {
            //TODO:
            //switch(resource)
            //{
            //    case Resource.Date:
            //        {

            //        } break;
            //    default:
            //        {

            //        } break;
            //}

            return false;
        }
    }

   

}

public enum Resource
{
    Allies,
    Axis,
    Soviets,
    Food,
    Iron,
    Money,
    Popularity,
    Date
}

public enum Compare
{
    Equal,
    Less,
    More
}
