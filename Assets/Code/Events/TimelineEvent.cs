using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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