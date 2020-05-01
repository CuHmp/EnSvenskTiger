﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Language
{
    English,
    Swedish,
    Hebrew,
    Count


}



public class GameSettings : ScriptableObject
{
    static Language lang = Language.English;
    
    public static void SetLang(Language newLang)
    {
        if (newLang != Language.Count)
        {
            lang = newLang;
        }
    }

    public static Language GetLang()
    {
        return lang;
    }

    public static int GetLangInt()
    {
        return (int)lang;
    }
}
