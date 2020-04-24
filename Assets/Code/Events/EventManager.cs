using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventManager : ManagerManager
{
    //TimeMaster tm;

    List<TimelineEvent> TimelineEvents;

    List<ConditionalTimelineEvent> ConditionalTimelineEvents;

    List<RandomEvent> RandomEvents;


    List<GameEvent> PastEvents = new List<GameEvent>();
    List<GameEvent> EventQueue = new List<GameEvent>();

    [SerializeField]
    EventWindow eventWindow;
    int[] RandomEventDates = { 1, 4, 7, 10 };

    public UIManager UI;

    void Awake()
    {
        eventWindow = GameObject.Find("EventWindow").GetComponent<EventWindow>();

        eventWindow.gameObject.SetActive(false);


        Debug.Log("EventManager Created");
    }

    public override bool Init() {

        TimelineEvents = new List<TimelineEvent>(Resources.LoadAll<TimelineEvent>("TimelineEvents"));
        ConditionalTimelineEvents = new List<ConditionalTimelineEvent>(Resources.LoadAll<ConditionalTimelineEvent>("ConditionalTimelineEvents"));
        RandomEvents = new List<RandomEvent>(Resources.LoadAll<RandomEvent>("RandomEvents"));
        UI = FindObjectOfType<UIManager>();

        TimeMaster.onTick.AddListener(EventChecker);

        Debug.Log("EventManager Initialized");

        return true;
    }

    void Update()
    {
        if (EventQueue.Count != 0 && !eventWindow.gameObject.activeSelf)
        {
            ExecuteEvent(EventQueue[0]);
        }
    }

    public void RemoveEventFromQueue(GameEvent e)
    {
        EventQueue.Remove(e);
        UI = FindObjectOfType<UIManager>();
        UI.UpdateStats();
    }

    void ExecuteEvent(GameEvent e)
    {
        eventWindow.gameObject.SetActive(true);
        eventWindow.LaunchEvent(e);
        AddToPastEvents(e);
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

        foreach (ConditionalTimelineEvent e in ConditionalTimelineEvents) {
            if (e.GetDate() == currentDate && !HasHappened(e)) {
                GameEvent ge = ScriptableObject.CreateInstance<GameEvent>();
                ge.setEvent(e, e.ChooseEffects());
                AddEventToQueue(ge);
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
