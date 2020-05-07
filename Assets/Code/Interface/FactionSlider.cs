using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionSlider : MonoBehaviour
{
    [SerializeField]
    Slider OpinionSlider, PowerSlider;

    public Image PowerFill;
    float fade;

    bool InDanger = false;

    void Awake()
    {
        fade = PowerFill.color.a;
        OpinionSlider.onValueChanged.AddListener(OnChange);
        PowerSlider.onValueChanged.AddListener(OnChange);
    }

    void Update()
    {
        if (InDanger)
        {
            Color temp = PowerFill.color;
            temp.a = fade + Mathf.PingPong(Time.time * 2, 0.9f - fade);
            PowerFill.color = temp;
        }
    }

    void OnChange(float f)
    {
        if (OpinionSlider.value < PowerSlider.value)
        {
            InDanger = true;
        }
        else
        {
            InDanger = false;
            PowerFill.color = new Color(PowerFill.color.r, PowerFill.color.g, PowerFill.color.b, fade);
        }
    }

    public void SetOpinion(int opinion)
    {
        OpinionSlider.value = opinion;
    }

    public void SetPower(int power)
    {
        PowerSlider.value = power;
    }
}
