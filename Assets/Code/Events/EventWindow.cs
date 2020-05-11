using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventWindow : MonoBehaviour
{
    [SerializeField]
    GameObject[] Options = new GameObject[2];

    [SerializeField]
    Text Title, Description;

    [SerializeField]
    [LabeledArray(typeof(Resource))]
    Sprite[] ResourceIcons = new Sprite[(int)Resource.Count];

    [SerializeField]
    Image[] EffectIcons = new Image[4], EffectIconsOp1 = new Image[4], EffectIconsOp2 = new Image[4];


    GameEvent Event;
    EventManager em;

    List<Effect>[] OpEffects = new List<Effect>[2];
    List<Effect> ConstEffects;


    public GameEvent tester;

    static int WaitMonth = 999;
    static int CurrentMonth = 999;
    static bool is_activated = false;
    public void LaunchEvent(GameEvent e)
    {
        Event = e;
        Title.text = "";
        if (e.GetType() == typeof(RandomEvent))
        {
            Title.text = "[Random Event] ";
        }
        Title.text = Title.text + e.GetTitle();
        Description.text = e.GetFlavorText();
        ConstEffects = e.GetEffects();
        DisplayEffects(ConstEffects, EffectIcons);
        EventChoice[] EventOptions = e.GetOptions();
        foreach (GameObject go in Options)
        {
            go.SetActive(false);
        }
        for (int i = 0; i < EventOptions.Length; i++)
        {
            Options[i].SetActive(true);
            Options[i].GetComponentInChildren<Text>().text = EventOptions[i].GetFlavorText();
            OpEffects[i] = EventOptions[i].GetEffects();
            if (i == 0) 
            {
                DisplayEffects(OpEffects[i], EffectIconsOp1); 
            }
            else if (i == 1)
            {
                DisplayEffects(OpEffects[i], EffectIconsOp2);
            }
        }

    }

    void OnEnable()
    {
        TimeMaster.TogglePlay(false);
        WaitMonth = 2;
        is_activated = true;
    }

    public void AddListner() {
        TimeMaster.onTick.AddListener(Tick);
        WaitMonth = 9999;
    }

    private void Tick() {
        if(WaitMonth > 0 && CurrentMonth != TimeMaster.GetTime().Month && is_activated) {
            WaitMonth--;
            CurrentMonth = TimeMaster.GetTime().Month;
        }
        if(WaitMonth == 0) {
            ChooseOption(0);
            WaitMonth = 9999;
        }
    }

    private void OnDisable()
    {
        em = FindObjectOfType<EventManager>();
        OpEffects = new List<Effect>[2];
        ConstEffects = new List<Effect>();
        if (em != null)
        {
            em.RemoveEventFromQueue(Event);
        }
        TimeMaster.TogglePlay(true);
        is_activated = false;
        UIManager UI = FindObjectOfType<UIManager>();
        if (UI != null)
        {
            UI.UpdateStats();
        }
    }

    public void ChooseOption(int index)
    {
        if (Event.GetType() == typeof(EndEvent))
        {
            SceneManager.LoadSceneAsync(0);
        }
        Player.AddResources(OpEffects[index]);
        Player.AddResources(ConstEffects);
        gameObject.SetActive(false);
    }

    void DisplayEffects(List<Effect> Effects, Image[] TargetImages)
    {
        foreach (Image icon in TargetImages)
        {
            icon.gameObject.SetActive(false);
        }
        for (int i = 0; i < Effects.Count && i < TargetImages.Length; i++)
        {
            TargetImages[i].gameObject.SetActive(true);
            int index = Effects[i].GetResourceInt();
            TargetImages[i].gameObject.SetActive(true);
            TargetImages[i].GetComponentInChildren<Text>().text = Effects[i].GetValue().ToString("+#;-#;0");
            TargetImages[i].sprite = ResourceIcons[index];

        }
    }
}
 