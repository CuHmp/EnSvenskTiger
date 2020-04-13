using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
    Hashtable borderChanges = new Hashtable();

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
        borderChanges.Add(new System.DateTime(1939, 9, 17), "POL:GER");
        borderChanges.Add(new System.DateTime(1939, 3, 15), "CZE:GER");
        borderChanges.Add(new System.DateTime(1939, 3, 15), "SLO:HUN");
        borderChanges.Add(new System.DateTime(1940, 7, 21), "LIT:S-RUS");
        borderChanges.Add(new System.DateTime(1940, 6, 17), "LAT:S-RUS");
        borderChanges.Add(new System.DateTime(1940, 6, 17), "EST:S-RUS");
        borderChanges.Add(new System.DateTime(1940, 4, 9), "DEN:GER");
        borderChanges.Add(new System.DateTime(1940, 4, 15), "NOR:GER");
        borderChanges.Add(new System.DateTime(1940, 5, 28), "BEL:GER");
        borderChanges.Add(new System.DateTime(1940, 5, 17), "HOL:GER");
        borderChanges.Add(new System.DateTime(1940, 6, 14), "FRA:GER");
        borderChanges.Add(new System.DateTime(1941, 7, 9), "LIT:GER");
        borderChanges.Add(new System.DateTime(1941, 7, 9), "LAT:GER");
        borderChanges.Add(new System.DateTime(1941, 9, 1), "EST:GER");
        borderChanges.Add(new System.DateTime(1941, 9, 1), "S-BEL:GER");
        borderChanges.Add(new System.DateTime(1941, 9, 26), "S-UKR:GER");
        borderChanges.Add(new System.DateTime(1941, 9, 1), "S-MOL:ROM");

        borderChanges.Add(new System.DateTime(1944, 4, 30), "S-UKR:S-RUS");
        borderChanges.Add(new System.DateTime(1944, 8, 19), "S-BEL:S-RUS");
        borderChanges.Add(new System.DateTime(1944, 8, 25), "S-MOL:S-RUS");
        borderChanges.Add(new System.DateTime(1944, 8, 30), "ROM:S-RUS");
        borderChanges.Add(new System.DateTime(1944, 12, 31), "LIT:S-RUS");
        borderChanges.Add(new System.DateTime(1944, 12, 31), "LAT:S-RUS");
        borderChanges.Add(new System.DateTime(1944, 12, 31), "EST:S-RUS");
        borderChanges.Add(new System.DateTime(1944, 12, 31), "HUN:S-RUS");
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
        if(from.ToUpper() == "POL") {
            int OLD_BORDERS_index = FindIndex("W-POL");
            int NULL_index = FindIndex("NULL");
            instance.StartCoroutine(ChangeColourGradually(OLD_BORDERS_index, NULL_index, 1.5f));
        }

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
        Debug.LogWarning("Error 001: Country not found");
        return -1;
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
