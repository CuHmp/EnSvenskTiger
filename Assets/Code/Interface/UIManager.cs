using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    FactionSlider Allies, Axis, Soviets;

    [SerializeField]
    Slider Popularity;

    [SerializeField]
    ResourceCounter Food, Iron, Money;
    void Start()
    {
        UpdateStats();
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
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

        Food.SetValue(Player.GetResource(Resource.Food));
        Iron.SetValue(Player.GetResource(Resource.Iron));
        Money.SetValue(Player.GetResource(Resource.Money));
    }
}
