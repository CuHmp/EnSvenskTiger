using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopSlider : MonoBehaviour
{
    Slider slider;
    Color green = new Color(0.3058824f, 0.7372549f, 0), 
          yellow = new Color(1, 0.9529412f, 0), 
          red = new Color(0.7f, 0, 0, 0.3f);



    public Image RedZone, Fill;

    bool InDanger = false;


    void Awake()
    {
        RedZone.color = red;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSlider);
    }

    void Update()
    {
        if (InDanger)
        {
            float offset = 0.3f;
            Color temp = red;
            temp.a = offset + Mathf.PingPong(Time.time * 2, 1.0f - offset);
            RedZone.color = temp;
        }
    }

    void OnSlider(float i)
    {
        i = (int)i;

        if (i >= 4)
        {
            Fill.color = green;
            InDanger = false;
            RedZone.color = red;
        }
        else
        {
            Fill.color = yellow;
            InDanger = true;
        }
    }
}
