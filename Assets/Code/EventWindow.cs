﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    Image[] EffectIcons = new Image[3], EffectIconsOp1 = new Image[3], EffectIconsOp2 = new Image[3];


    GameEvent Event;
    EventManager em;

    List<Effect>[] OpEffects = new List<Effect>[2];
    List<Effect> ConstEffects;


    public GameEvent tester;

    void Awake()
    {
        em = FindObjectOfType<EventManager>();




    }

    public void LaunchEvent(GameEvent e)
    {
        Event = e;
        Title.text = e.GetTitle();
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
    }

    

    private void OnDisable()
    {
        OpEffects = new List<Effect>[2];
        ConstEffects = new List<Effect>();
        em.RemoveEventFromQueue(Event);
        TimeMaster.TogglePlay(true);
    }

    public void ChooseOption(int index)
    {
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
 