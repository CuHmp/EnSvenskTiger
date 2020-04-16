using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static int[] Resources = new int[(int)Resource.Count];

    [SerializeField]
    GameEvent initVals;

 
    void Awake()
    {
        SetResources(initVals.GetEffects());
    }


    public static void SetResources(List<Effect> effects)
    {
        foreach (Effect e in effects)
        {
            if (e.GetResource() != Resource.Count)
            {
                Resources[(int)e.GetResource()] = e.GetValue();
            }
        }

        ClampResources();
    }

    public static void AddResources(List<Effect> effects)
    {
        foreach (Effect e in effects)
        {
            if (e.GetResource() != Resource.Count)
            {
                Resources[(int)e.GetResource()] += e.GetValue();
            }
        }

        ClampResources();
    }

    static void ClampResources()
    {
        foreach (int i in Resources)
        {
            Mathf.Clamp(i, 0, 10);
        }
    }
}
