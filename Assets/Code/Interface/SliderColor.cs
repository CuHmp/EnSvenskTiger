using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderColor : MonoBehaviour
{
    [SerializeField]
    Image face;
    [SerializeField]
    Slider slider;

    Color green = Color.green, red = Color.red;
    void FixedUpdate()
    {
        face.color = Color.Lerp(red, green, slider.normalizedValue);
    }
}
