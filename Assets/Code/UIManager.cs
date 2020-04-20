using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    FactionSlider Allies, Axis, Soviets;

    [SerializeField]
    Slider Popularity;
    void Start()
    {
        UpdateStats();
    }

    

    public void UpdateStats()
    {
        Allies.SetOpinion(Player.GetResource(Resource.AlliesOpinion));
        Allies.SetPower(Player.GetResource(Resource.AlliesPower));

        Axis.SetOpinion(Player.GetResource(Resource.AxisOpinion));
        Axis.SetPower(Player.GetResource(Resource.AxisPower));

        Soviets.SetOpinion(Player.GetResource(Resource.SovietOpinion));
        Soviets.SetPower(Player.GetResource(Resource.SovietPower));

        Popularity.value = Player.GetResource(Resource.Popularity);

        // TODO: Iron, money and food
    }
}
