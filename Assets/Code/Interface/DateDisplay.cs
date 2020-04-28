using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateDisplay : MonoBehaviour
{
    [SerializeField]
    Text text;

    [SerializeField]
    Button butt;
    void Start()
    {
        TimeMaster.onTick.AddListener(DateTick);
        butt.onClick.AddListener(TogglePause);
        TimeMaster.onPause.AddListener(SetButtonImage);
        SetButtonImage();
        
    }

    void DateTick()
    {
        text.text = TimeMaster.GetTime().Date.ToString("dd/MM/yyyy");
    }

    void TogglePause()
    {
        if (TimeMaster.IsPaused())
        {
            TimeMaster.TogglePlay(false);
        }
        else
        {
            TimeMaster.TogglePlay(true);
        }

    }

    void SetButtonImage()
    {
        Text temp = butt.gameObject.GetComponentInChildren<Text>();
        if (TimeMaster.IsPaused())
        {
            temp.text = ">";
        }
        else
        {
            temp.text = "||";
        }
    }


}
