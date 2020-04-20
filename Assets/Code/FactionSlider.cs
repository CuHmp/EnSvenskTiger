using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionSlider : MonoBehaviour
{
    [SerializeField]
    Slider OpinionSlider, PowerSlider;

    public void SetOpinion(int opinion)
    {
        OpinionSlider.value = opinion;
    }

    public void SetPower(int power)
    {
        PowerSlider.value = power;
    }
}
