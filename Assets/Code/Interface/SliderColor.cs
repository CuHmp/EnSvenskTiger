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

    Color green = Color.green, red = Color.red, yellow = Color.yellow;
    void FixedUpdate()
    {

        Color temp = Color.black;

        temp.g = Mathf.Lerp(0.0f, 1.0f, slider.normalizedValue * 2);
        temp.r = Mathf.Lerp(1.0f, 0.0f, (slider.normalizedValue - 0.5f) * 2);

        face.color = temp;

    }
}
