using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField]
    Text resourceValue;

    [SerializeField]
    Button butt;

    [SerializeField]
    ResourceEvent re;

    void Awake()
    {
        butt.onClick.AddListener(LaunchEvent);
    }

    public void SetValue(int value)
    {
        resourceValue.text = value.ToString();
    }

    void LaunchEvent()
    {
        EventManager.CreateResourceEvent(re);
    }
}
