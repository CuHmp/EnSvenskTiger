using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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
