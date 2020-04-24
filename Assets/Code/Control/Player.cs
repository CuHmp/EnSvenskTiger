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

    public static int GetResource(Resource type) {
        return Resources[(int)type];
    }
    public static int GetResource(int type) {
        return Resources[type];
    }

    public static void AddResource(Resource type, int value) {
        Resources[(int)type] += value;
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

        //ClampResources();
    }

    public static void ClampResources()
    {
        for (int i = 0; i < (int)Resource.Count; i++)
        {
            Resources[i] = Mathf.Clamp(Resources[i], 0, 10);
        }
    }
}
