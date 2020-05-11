using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : ManagerManager {
    private static int current_month_ = 1;
    private static int graze_period_ = 9999;
    private static readonly int frequency_tribute_sending_ = 2;
    private static readonly int start_graze_time_ = 4;

    private static readonly string[] nations_opinion_ = { "ALLIED", "AXIS", "SOVIET" };
    private static UIManager UI_;

    private static Dictionary<string, int> tribute_duration_ = new Dictionary<string, int>();
    private static Dictionary<string, int> nation_opinion_ = new Dictionary<string, int>();

    private void Awake() {
        Button[] options = GameObject.Find("EventWindow").GetComponentsInChildren<Button>();
        foreach (Button b in options) {
            b.onClick.AddListener(EventFinished);
        }

        Debug.Log("ResorceManager Created");
    }

    public override bool Init() {
        
        tribute_duration_.Add("ALLIED", -1);
        tribute_duration_.Add("AXIS", -1);
        tribute_duration_.Add("SOVIET", -1);

        nation_opinion_.Add("ALLIED", Player.GetResource(Resource.AlliesOpinion));
        nation_opinion_.Add("AXIS", Player.GetResource(Resource.AxisOpinion));
        nation_opinion_.Add("SOVIET", Player.GetResource(Resource.SovietOpinion));
        

        TimeMaster.onTick.AddListener(Tick);
        UI_ = FindObjectOfType<UIManager>();

        Debug.Log("ResorceManager Initialized");
        return true;
    }

    void Tick() {
        if(current_month_ != TimeMaster.GetTime().Month) { // month counter
            current_month_ = TimeMaster.GetTime().Month;

            graze_period_--;
            if (graze_period_ == 0) { // you get invaded by another nation
                ColorManager.GameOver();
            }

            CheckTribute();
            CheckGameCriticalResources();
        }
    }

    static void CheckTribute() {
        foreach (string nationName in nations_opinion_) {
            string upperNationName = nationName.ToUpper();
            if (tribute_duration_[upperNationName] > 0) {
                tribute_duration_[upperNationName]--;
                if (tribute_duration_[upperNationName] == 0) {
                    switch (upperNationName) {
                        case "ALLIED": 
                        {
                            Player.AddResource(Resource.Money, -1);
                            tribute_duration_[upperNationName] = frequency_tribute_sending_;
                            break;
                        }
                        case "AXIS": 
                        {
                            Player.AddResource(Resource.Iron, -1);
                            tribute_duration_[upperNationName] = frequency_tribute_sending_;
                            break;
                        }
                        case "SOVIET": 
                        {
                            Player.AddResource(Resource.Food, -1);
                            tribute_duration_[upperNationName] = frequency_tribute_sending_;
                            break;
                        }
                    }
                    UI_.UpdateStats();
                }
            }
        }
    }

    static void EventFinished() {
        CheckGameCriticalResources();
        CheckOpinion();

        Player.ClampResources();
    }

    static void CheckGameCriticalResources() {
        if (Player.GetResource(Resource.Popularity) <= 0) {
            ColorManager.GameOver();
        }
        if (Player.GetResource(Resource.Food) <= 0) {
            ColorManager.GameOver();
        }
        if (Player.GetResource(Resource.Iron) <= 0) {
            ColorManager.GameOver();
        }
        if (Player.GetResource(Resource.Money) <= 0) {
            ColorManager.GameOver();
        }
    }

    static void CheckOpinion() {
        int[]  nationPower = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        bool[] lowOpinion  = { false, false, false };

        for(int i = 0; i < nations_opinion_.Length; i++) {

            string upperNation = nations_opinion_[i].ToUpper();
            nation_opinion_[upperNation] = Player.GetResource(i * 2);
            nationPower[i] = Player.GetResource((i * 2) + 1);
            int nationOpinion = nation_opinion_[upperNation];
            
            lowOpinion[i] = CheckLowOpinion(nationOpinion, nationPower[i]);
            CheckHighOpinion(upperNation, nationOpinion);
        }

        NoOpinionAboveZero(lowOpinion);
    }

    private static void NoOpinionAboveZero(bool[] _low_opinions) { // kills the graze period if no opinion is zero
        bool notZero = false;
        foreach (bool opinionIsZero in _low_opinions) {
            if (opinionIsZero) {
                notZero = true;
                break;
            }
            else {
                notZero = false;
            }
        }
        if (!notZero) {
            graze_period_ = 9999;
        }
    }

    private static bool CheckLowOpinion(int _nation_opinion, int _nation_power) {
        if (_nation_opinion < _nation_power) {
            if(graze_period_ > start_graze_time_ * 2) {  // makes sure that a graze period countdown has not started
                graze_period_ = start_graze_time_;
            }
            return true;
        }
        return false;
    }

    private void OnDisable() {
        tribute_duration_ = new Dictionary<string, int>();
        nation_opinion_ = new Dictionary<string, int>();
    }

    private static void CheckHighOpinion(string _nation_name, int _nation_opinion) {
        if(_nation_opinion > 8) {
            if (tribute_duration_[_nation_name] < 0) { // makes sure that a tribute countdown has not started
                tribute_duration_[_nation_name] = frequency_tribute_sending_;
            }
        }
        else {
            tribute_duration_[_nation_name] = -1;
        }
    }
}