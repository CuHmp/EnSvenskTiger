using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventManager : ManagerManager
{

    List<TimelineEvent> TimelineEvents;

    List<ConditionalTimelineEvent> ConditionalTimelineEvents;

    List<RandomEvent> RandomEvents;


    List<GameEvent> PastEvents = new List<GameEvent>();
    static List<GameEvent> EventQueue = new List<GameEvent>();

    [SerializeField]
    static EventWindow eventWindow = null;
    int[] RandomEventDates = { 1, 4, 7, 10 };

    static DateTime lastResourceEvent = new DateTime();
    static readonly int CooldownMonths = 1;
    public UIManager UI;

    void Awake()
    {
        EventQueue = new List<GameEvent>();
        Debug.Log("EventManager Created");
        lastResourceEvent = new DateTime();
    }

    public override bool Init() {

        TimelineEvents = new List<TimelineEvent>(Resources.LoadAll<TimelineEvent>("TimelineEvents"));
        ConditionalTimelineEvents = new List<ConditionalTimelineEvent>(Resources.LoadAll<ConditionalTimelineEvent>("ConditionalTimelineEvents"));
        RandomEvents = new List<RandomEvent>(Resources.LoadAll<RandomEvent>("RandomEvents"));
        UI = FindObjectOfType<UIManager>();
        if (eventWindow == null) {
            eventWindow = FindObjectOfType<EventWindow>();
        }
        eventWindow.AddListner();
        eventWindow.gameObject.SetActive(false);
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
        
    }

    void ExecuteEvent(GameEvent e)
    {
        eventWindow.gameObject.SetActive(true);
        eventWindow.LaunchEvent(e);
        if (e.GetType() != typeof(ResourceEvent))
        {
            AddToPastEvents(e);
        }
        else
        {
            lastResourceEvent = TimeMaster.GetTime();
        }
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

    static void AddEventToQueue(GameEvent e)
    {
        EventQueue.Add(e);
    }

    public static void CreateResourceEvent(ResourceEvent re)
    {
        DateTime temp = lastResourceEvent.AddMonths(CooldownMonths);
        if (temp <= TimeMaster.GetTime())
        {
            AddEventToQueue(re);
        }
    }

}
