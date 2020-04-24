using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
// 1. JAN, 2. FEB, 3. MAR, 4. APR, 5. MAY, 6. JUN, 7. JUL, 8. AUG, 9. SEP, 10. OCT, 11. NOV, 12. DEC
public class ColorManager : ManagerManager {
    private static Hashtable timeline_border_changes_ = new Hashtable();
    private static List<GameObject> countries_ = null;

    public static ColorManager instance_;

    private void Awake() {
        instance_ = this;

        Debug.Log("ColorManager Created");
        return;
    }

    public override bool Init() {
        AddCountries();
        ParseFile();

        TimeMaster.onTick.AddListener(Tick);

        Debug.Log("ColorManager Initialized");

        return true;
    }
    private static void AddCountries() {
        countries_ = new List<GameObject>();
        Transform mapTransform = GameObject.Find("Map").transform;
        foreach (Transform child in mapTransform) {
            countries_.Add(child.gameObject);
        }
    }
    private static void ParseFile() {
        string file_content = Readfile("Assets/Resources/BorderChanges.txt");
        string[] lines = file_content.Split('\n');
        foreach (string text in lines) {
            string[] date_and_countries = text.Split('|');
            // Removes the invisiable \n or break character
            date_and_countries[1] = date_and_countries[1].Remove(date_and_countries[1].Length - 1); 
            timeline_border_changes_.Add(System.DateTime.Parse(date_and_countries[0]), date_and_countries[1]);
        }
    }

    private static string Readfile(string path) {
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();
        return content;
    }

    public static void GameOver() {
        ChangeControl("GAMEOVER", "SOLID");
        TimeMaster.TogglePlay(false);
    }

    public static bool ChangeControl(string _from, string _to) {
        int target_index = FindIndex(_from);
        int goal_index = FindIndex(_to);
        if(target_index < 0 || goal_index < 0) {
            return false;
        }
        instance_.StartCoroutine(ChangeColourGradually(target_index, goal_index));
        return true;
    }

    private static int FindIndex(string _name) {
        for(int i = 0; i < countries_.Count; i++) {
            if(countries_[i].name.Equals(_name)) {
                return i;
            }
        }
        Debug.LogWarning("Error 001: Country not found: " + _name + " breaking away from changing color of map");
        return -1;
    }

    static void Tick() {
        if (timeline_border_changes_.ContainsKey(TimeMaster.GetTime())) {
            string[] borderChanges = timeline_border_changes_[TimeMaster.GetTime()].ToString().Split(':');
            ChangeControl(borderChanges[0].ToUpper(), borderChanges[1].ToUpper());
        }
    }

    private static IEnumerator ChangeColourGradually(int _target, int _goal) {
        float tick = 0f;
        Color startColor = countries_[_target].GetComponent<SpriteRenderer>().color;
        Color endColor = countries_[_goal].GetComponent<SpriteRenderer>().color;

        while (countries_[_target].GetComponent<SpriteRenderer>().color != endColor) {
            tick += Time.deltaTime * 1.0f;
            countries_[_target].GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, countries_[_goal].GetComponent<SpriteRenderer>().color, tick);
            yield return null;
        }
    }
}