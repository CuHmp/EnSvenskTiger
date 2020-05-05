using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class LangString 
{
    [SerializeField]
    [LabeledArray(typeof(Language))]
    string[] text = new string[(int)Language.Count];

    

    static bool[] rightToLeft = new bool[(int)Language.Count] { false, false/*, true*/ };

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

    public string GetText(Language lang)
    {
        return text[(int)lang];
    }

    public void SetText(string s, Language lang)
    {
        text[(int)lang] = s;
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
