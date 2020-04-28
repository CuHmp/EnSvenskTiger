using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateDisplay : MonoBehaviour
{
    [SerializeField]
    Text text;

    [SerializeField]
    Button playPause, speedUp, speedDown;
    void Start()
    {
        TimeMaster.onTick.AddListener(DateTick);
        playPause.onClick.AddListener(TogglePause);
        speedUp.onClick.AddListener(IncreaseSpeed);
        speedDown.onClick.AddListener(DecreaseSpeed);
        TimeMaster.onPause.AddListener(SetButtonImage);
        SetButtonImage();
        
    }

    void DateTick()
    {
        text.text = TimeMaster.GetTime().Date.ToString("dd/MM/yyyy");
    }

    void TogglePause()
    {
        TimeMaster.TogglePlay(TimeMaster.IsPaused());


    }

    void SetButtonImage()
    {
        Text temp = playPause.gameObject.GetComponentInChildren<Text>();
        if (TimeMaster.IsPaused())
        {
            temp.text = ">";
        }
        else
        {
            temp.text = "||";
        }
    }

    void IncreaseSpeed()
    {
        TimeMaster.IncreaseSpeed();
    }

    void DecreaseSpeed()
    {
        TimeMaster.DecreaseSpeed();
    }
}
