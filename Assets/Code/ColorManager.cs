using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 1. JAN, 2. FEB, 3. MAR, 4. APR, 5. MAY, 6. JUN, 7. JUL, 8. AUG, 9. SEP, 10. OCT, 11. NOV, 12. DEC
public class ColorManager : MonoBehaviour {
    Hashtable TimelineBorderChanges = new Hashtable();

    public static ColorManager instance;
    private static List<GameObject> countries = null;



    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
            ColorManager.init();
        }
        TimeMaster time_master = FindObjectOfType<TimeMaster>();
        time_master.onTick.AddListener(Tick);
        {
            TimelineBorderChanges.Add(new System.DateTime(1939, 3, 15), "CZH:GER");
            TimelineBorderChanges.Add(new System.DateTime(1939, 3, 16), "SLO:HUN");
            TimelineBorderChanges.Add(new System.DateTime(1939, 9, 28), "G-POL:GER");
            TimelineBorderChanges.Add(new System.DateTime(1939, 9, 27), "S-POL:S-RUS");

            TimelineBorderChanges.Add(new System.DateTime(1940, 8, 3), "LIT:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1940, 8, 6), "EST:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1940, 8, 5), "LAT:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1940, 4, 9), "DEN:GER");
            TimelineBorderChanges.Add(new System.DateTime(1940, 5, 5), "NOR:GER");
            TimelineBorderChanges.Add(new System.DateTime(1940, 5, 11), "LUX:GER"); // 1942, 8, 30
            TimelineBorderChanges.Add(new System.DateTime(1940, 5, 28), "BEL:GER");
            TimelineBorderChanges.Add(new System.DateTime(1940, 5, 14), "HOL:GER");
            TimelineBorderChanges.Add(new System.DateTime(1940, 6, 25), "FRA:GER");

            TimelineBorderChanges.Add(new System.DateTime(1941, 4, 12), "YUG:GER");
            TimelineBorderChanges.Add(new System.DateTime(1941, 6, 27), "LIT:GER");
            TimelineBorderChanges.Add(new System.DateTime(1941, 7, 10), "LAT:GER");
            TimelineBorderChanges.Add(new System.DateTime(1941, 9, 5), "EST:GER");
            TimelineBorderChanges.Add(new System.DateTime(1941, 6, 20), "S-POL:GER");
            TimelineBorderChanges.Add(new System.DateTime(1941, 7, 9), "S-BEL:GER");
            TimelineBorderChanges.Add(new System.DateTime(1941, 9, 19), "S-UKR:GER");

            TimelineBorderChanges.Add(new System.DateTime(1943, 10, 13), "ITA:UK");

            TimelineBorderChanges.Add(new System.DateTime(1944, 7, 13), "LIT:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1944, 10, 5), "LAT:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1944, 4, 10), "S-UKR:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1944, 7, 3), "S-BEL:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1944, 8, 23), "ROM:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1944, 10, 6), "HUN:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1944, 10, 10), "SLO:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1944, 7, 14), "S-POL:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1944, 8, 1), "G-POL:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1944, 8, 25), "FRA:UK");
            TimelineBorderChanges.Add(new System.DateTime(1944, 9, 21), "EST:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1944, 9, 3), "BEL:UK");
            TimelineBorderChanges.Add(new System.DateTime(1944, 9, 10), "LUX:UK");
            TimelineBorderChanges.Add(new System.DateTime(1944, 10, 24), "HOL:UK");

            TimelineBorderChanges.Add(new System.DateTime(1945, 5, 25), "YUG:UK");
            TimelineBorderChanges.Add(new System.DateTime(1945, 5, 11), "CZH:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1945, 4, 13), "AUS:S-RUS");
            TimelineBorderChanges.Add(new System.DateTime(1945, 4, 27), "NOR:UK");
            TimelineBorderChanges.Add(new System.DateTime(1945, 3, 21), "DEN:UK");
        }
    }

    public static void init() {
        countries = new List<GameObject>();
        Transform mapTransform = GameObject.Find("Map").transform;
        foreach (Transform child in mapTransform) {
            countries.Add(child.gameObject);
            Debug.Log(child.gameObject.name);
        }

    }

    public static bool ChangeControl(string from, string to) {
        int target_index = FindIndex(from.ToUpper());
        int goal_index = FindIndex(to.ToUpper());
        if(target_index < 0 || goal_index < 0) {
            return false;
        }
        instance.StartCoroutine(ChangeColourGradually(target_index, goal_index, 1f));
        return true;
    }

    private static int FindIndex(string name) {
        for(int i = 0; i < countries.Count; i++) {
            if(countries[i].name == name) {
                return i;
            }
        }
        Debug.LogWarning("Error 001: Country not found: " + name);
        return -1;
    }

    void Tick() {
        if (TimelineBorderChanges.ContainsKey(TimeMaster.GetTime())) {
            string[] borderChanges = TimelineBorderChanges[TimeMaster.GetTime()].ToString().Split(':');
            ChangeControl(borderChanges[0], borderChanges[1]);
        }
    }

    private static IEnumerator ChangeColourGradually(int target, int goal, float speed) {
        float tick = 0f;
        Color start_color = countries[target].GetComponent<SpriteRenderer>().color;
        Color StartColor = new Color(start_color.r, start_color.g, start_color.b);
        Color end_color = countries[goal].GetComponent<SpriteRenderer>().color;

        while (countries[target].GetComponent<SpriteRenderer>().color != end_color) {
            tick += Time.deltaTime * speed;
            countries[target].GetComponent<SpriteRenderer>().color = Color.Lerp(StartColor, countries[goal].GetComponent<SpriteRenderer>().color, tick);
            yield return null;
        }
    }

}
