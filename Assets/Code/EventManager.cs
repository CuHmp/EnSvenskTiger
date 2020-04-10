using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventManager : MonoBehaviour
{
    TimeMaster tm;

    List<TimelineEvent> TimelineEvents = new List<TimelineEvent>(Resources.LoadAll<TimelineEvent>("Assets/TimelineEvents"));

    List<RandomEvent> RandomEvents = new List<RandomEvent>(Resources.LoadAll<RandomEvent>("Assets/RandomEvents"));


    List<GameEvent> PastEvents;
    List<GameEvent> EventQueue;
    int[] RandomEventDates = { 1, 4, 7, 10 };
    void Awake()
    {
        tm = FindObjectOfType<TimeMaster>();
        tm.onTick.AddListener(EventChecker);

    }

    void Update()
    {
        if (EventQueue.Count != 0)
        {
            ExecuteEvent(EventQueue[0]);
        }
    }

    void ExecuteEvent(GameEvent e)
    {
        if (e.GetType() == typeof(TimelineEvent))
        {
            ExecuteEvent((TimelineEvent)e);
        }
        else if (e.GetType() == typeof(RandomEvent))
        {
            ExecuteEvent((RandomEvent)e);
        }
    }


    void ExecuteEvent(TimelineEvent e)
    {
        // TODO
    }

    void ExecuteEvent(RandomEvent e)
    {
        // TODO
    }



    void EventChecker()
    {
        DateTime currentDate = TimeMaster.GetTime();

        foreach (TimelineEvent e in TimelineEvents)
        {
            if (e.GetDate() == currentDate && !HasHappened(e))
            {
                AddEventToQueue(e);
                
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

    bool HasHappened(GameEvent e)
    {
        foreach (GameEvent ge in PastEvents)
        {
            if (ge == e)
            {
                return true;
            }
        }
        return false;
    }

    void RandomEvent()
    {
        RandomEvent re;
        do
        {
            re = RandomEvents[UnityEngine.Random.Range(0, RandomEvents.Count)];
        } while (HasHappened(re) || !re.GetCondition().IsMet());

        AddEventToQueue(re);
    }

    void AddToPastEvents(GameEvent e)
    {
        PastEvents.Add(e);
    }

    void AddEventToQueue(GameEvent e)
    {
        EventQueue.Add(e);
    }

}
