using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "GameEvent/Event", order = 1)]
public class GameEvent : ScriptableObject
{
    

    public EventChoice[] Options;

    public string title, flavorText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
