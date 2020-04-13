using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventWindow : MonoBehaviour
{
    [SerializeField]
    GameObject[] Options = new GameObject[2];
    [SerializeField]
    Text Title, Description;

    
    void Awake()
    {
       
    }

    

    public void LaunchEvent(GameEvent e)
    {
        Title.text = e.GetTitle();
        Description.text = e.GetFlavorText();
        EventChoice[] EventOptions = e.GetOptions();
        foreach (GameObject go in Options)
        {
            go.SetActive(false);
        }
        for (int i = 0; i < EventOptions.Length; i++)
        {
            Options[i].SetActive(true);
            Options[i].GetComponentInChildren<Text>().text = EventOptions[i].GetFlavorText();
        }

    }
}
