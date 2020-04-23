using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
// 1. JAN, 2. FEB, 3. MAR, 4. APR, 5. MAY, 6. JUN, 7. JUL, 8. AUG, 9. SEP, 10. OCT, 11. NOV, 12. DEC
public class ColorManager : ManagerManager {
    static Hashtable TimelineBorderChanges = new Hashtable();

    public static ColorManager instance;
    private static List<GameObject> countries = null;

    private void Awake() {
        instance = this;

        Debug.Log("ColorManager Created");
        return;
    }

    public override bool init() {
        AddCountries();
        parseFile();

        TimeMaster time_master = FindObjectOfType<TimeMaster>();
        time_master.onTick.AddListener(Tick);

        Debug.Log("ColorManager Initialized");

        return true;
    }
    private static void AddCountries() {
        countries = new List<GameObject>();
        Transform mapTransform = GameObject.Find("Map").transform;
        foreach (Transform child in mapTransform) {
            countries.Add(child.gameObject);
        }
    }
    private static void parseFile() {
        string file_content = readfile("Assets/Resources/BorderChanges.txt");
        string[] lines = file_content.Split('\n');
        foreach (string text in lines) {
            string[] date_and_countries = text.Split('|');
            date_and_countries[1] = date_and_countries[1].Remove(date_and_countries[1].Length - 1); // Removes the \n or break character
            TimelineBorderChanges.Add(System.DateTime.Parse(date_and_countries[0]), date_and_countries[1]);
        }
    }

    private static string readfile(string path) {
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();
        return content;
    }

    public static bool ChangeControl(string from, string to) {
        int target_index = FindIndex(from);
        int goal_index = FindIndex(to);
        if(target_index < 0 || goal_index < 0) {
            return false;
        }
        instance.StartCoroutine(ChangeColourGradually(target_index, goal_index));
        return true;
    }

    private static int FindIndex(string name) {
        for(int i = 0; i < countries.Count; i++) {
            if(countries[i].name.Equals(name)) {
                return i;
            }
        }
        Debug.LogWarning("Error 001: Country not found: " + name + " breaking away from changing color of map");
        return -1;
    }

    static void Tick() {
        if (TimelineBorderChanges.ContainsKey(TimeMaster.GetTime())) {
            string[] borderChanges = TimelineBorderChanges[TimeMaster.GetTime()].ToString().Split(':');
            ChangeControl(borderChanges[0].ToUpper(), borderChanges[1].ToUpper());
        }
    }

    private static IEnumerator ChangeColourGradually(int target, int goal) {
        float tick = 0f;
        Color StartColor = countries[target].GetComponent<SpriteRenderer>().color;
        Color end_color = countries[goal].GetComponent<SpriteRenderer>().color;

        while (countries[target].GetComponent<SpriteRenderer>().color != end_color) {
            tick += Time.deltaTime * 1.0f;
            countries[target].GetComponent<SpriteRenderer>().color = Color.Lerp(StartColor, countries[goal].GetComponent<SpriteRenderer>().color, tick);
            yield return null;
        }
    }
}