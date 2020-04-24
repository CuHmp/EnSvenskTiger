using System.Collections;
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

    static bool[] rightToLeft = new bool[(int)Language.Count] { false, false, true };

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
        return rightToLeft[GameSettings.GetLangInt()];
    }

    string ReverseString(string text)
    {
        string newString = "";
        for (int i = text.Length - 1; i >= 0; i--)
        {
            newString += text[i];
        }

        return newString;
    }
}
