using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New GenericEvent", menuName = "GameEvent/GenericEvent", order = 0)]
[System.Serializable]
public class GameEvent : ScriptableObject
{
    [SerializeField]
    protected LangString title, flavorText;
    [SerializeField]
    protected EventChoice[] Options = new EventChoice[2];
    [SerializeField]
    protected List<Effect> Effects;
    
    public string GetTitle()
    {
        return title.GetText();
    }
    public LangString GetLangStringTitle() {
        return title;
    }
    public LangString GetLangStringFlavorText() {
        return flavorText;
    }
    public string GetFlavorText()
    {
        return flavorText.GetText();
    }

    public EventChoice[] GetOptions()
    {
        return Options;
    }

    public List<Effect> GetEffects()
    {
        return Effects;
    }
    
    public void setEvent(GameEvent e, List<Effect> effect) {
        title = e.GetLangStringTitle();
        flavorText = e.GetLangStringFlavorText();
        Options = e.GetOptions();
        Effects = effect;
    }

}







public enum Resource
{
    AlliesOpinion, AlliesPower,
    AxisOpinion, AxisPower,
    SovietOpinion, SovietPower,
    Food,
    Iron,
    Money,
    Popularity,
    Count
}