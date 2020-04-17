using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    static Language lang = Language.Hebrew;

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
