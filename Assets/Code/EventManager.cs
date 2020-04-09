using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    TimeMaster tm;

    [SerializeField]
    List<TimelineEvent> TimelineEvents;
    int[] RandomEventDates = { 1, 4, 7, 10 };
    void Awake()
    {
        tm = FindObjectOfType<TimeMaster>();
        tm.onTick.AddListener(EventChecker);

    }

    

    void EventChecker()
    {
        DateTime currentDate = TimeMaster.GetTime();

        foreach (TimelineEvent e in TimelineEvents)
        {
            if (e.GetDate() == currentDate)
            {
                TimelineEvent(e);
            }
        }

        foreach (int randomDate in RandomEventDates)
        {
            if (currentDate == new DateTime(currentDate.Year, randomDate, 1))
            {
                RandomEvent();
            }
        }


    }
    void RandomEvent()
    {

    }

    void TimelineEvent(TimelineEvent e)
    {

    }
}
