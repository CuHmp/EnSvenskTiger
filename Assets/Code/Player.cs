using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static int[] Resources = new int[(int)Resource.Size];

    [SerializeField]
    GameEvent initVals;

 
    void Awake()
    {
        SetVars(initVals.GetEffects());
    }


    void SetVars(List<Effect> effects)
    {
        foreach (Effect e in effects)
        {
            if (e.GetResource() != Resource.Size)
            {
                Resources[(int)e.GetResource()] = e.GetValue();
            }
        }
    }

    public static void AddToVars(List<Effect> effects)
    {
        foreach (Effect e in effects)
        {
            if (e.GetResource() != Resource.Size)
            {
                Resources[(int)e.GetResource()] += e.GetValue();
            }
        }
    }
}
