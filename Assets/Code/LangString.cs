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

[System.Serializable]
public class LangString 
{
    [SerializeField]
    [LabeledArray(typeof(Language))]
    string[] text = new string[(int)Language.Count];

    Language[] rightToLeft = { Language.Hebrew };
    
    public string GetText()
    {
        if (!IsRightToLeft())
        {
            return text[GameSettings.GetLangInt()];
        }
        else
        {
            return ReverseString(text[GameSettings.GetLangInt()]);
        }
    }

    bool IsRightToLeft()
    {
        foreach (Language lang in rightToLeft)
        {
            if (lang == GameSettings.GetLang())
            {
                return true;
            }
        }
        return false;
    }

    string ReverseString(string text)
    {
        string newString = "";
        for (int i = text.Length - 1; i >= 0; i--)
        {
            newString += text[i];
        }

        return newString;
        // NOTE: NEEDS TESTING
    }
}
