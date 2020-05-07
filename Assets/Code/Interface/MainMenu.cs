using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button StartButton, ExitButton;

    [LabeledArray(typeof(Language))]
    public Button[] LanguageButtons = new Button[(int)Language.Count];

    public LangString StartText, ExitText, LangText;

    public Text start, exit, lang;

    void Awake()
    {
        ResetAllText();
        ExitButton.onClick.AddListener(Application.Quit);
    }


    public void SetLang(int lang)
    {
        GameSettings.SetLang((Language)lang);
        ResetAllText();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void ResetAllText()
    {
        start.text = StartText.GetText();
        lang.text = LangText.GetText();
        exit.text = ExitText.GetText();
    }
}
