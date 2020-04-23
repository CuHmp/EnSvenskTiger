using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField]
    Text resourceValue;

    
   public void SetValue(int value)
    {
        resourceValue.text = value.ToString();
    }
}
