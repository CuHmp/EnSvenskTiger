using System.Collections;
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
    Image[] EffectIcons = new Image[3];

    List<Effect>[] OpEffects = new List<Effect>[2];
    List<Effect> ConstEffects;

    void Awake()
    {
        foreach (Image effect in EffectIcons)
        {
            gameObject.SetActive(false);
        }
    }

    public void LaunchEvent(GameEvent e)
    {
        Title.text = e.GetTitle();
        Description.text = e.GetFlavorText();
        ConstEffects = e.GetEffects();
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
        TimeMaster.TogglePlay(true);
    }

    public void ChooseOption(int index)
    {
        Player.AddResources(OpEffects[index]);
        Player.AddResources(ConstEffects);
        gameObject.SetActive(false);
    }

    void DesplayEffects(List<Effect> Effects)
    {
        for (int i = 0; i < Effects.Count && i < EffectIcons.Length; i++)
        {
            EffectIcons[i].sprite = ResourceIcons[Effects[i].GetResourceInt()];
        }
    }
}
