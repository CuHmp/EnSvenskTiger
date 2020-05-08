using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    float accumulator = 0.0f, speed = 50.0f;
    const float limit = 27.0f;

    [LabeledArray(new string[] {"Coding", "Concept"})]
    public LangString[] langStrings = new LangString[2];
    [LabeledArray(new string[] { "Coding", "Concept" })]
    public Text[] strings = new Text[2];


    void Awake()
    {
        for (int i = 0; i < langStrings.Length && i < strings.Length; i++)
        {
            strings[i].text = langStrings[i].GetText();
        }
    }
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
        accumulator += Time.deltaTime;
        if (accumulator > limit || Input.anyKey)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
