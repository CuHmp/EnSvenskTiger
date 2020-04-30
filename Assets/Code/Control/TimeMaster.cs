using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TimeMaster : ManagerManager {
    [Header("Events")]
    public static readonly TimeTicker onTick = new TimeTicker(); // event system
    public static readonly TimeTicker onPause = new TimeTicker(); // event system
    private static readonly System.DateTime end_date_ = new System.DateTime(1945, 9, 2);


    //date variables
    private static int month_ = 1;
    private static int day_ = 1;
    private static int year_ = 1939;

    //time tick variables
    private static readonly float wait_timer_ = 3.5f;
    private static float delta_time_ = 0;
    private static int[] speed_ = { 0, 2, 4, 6, 8, 12, 16 };
    private static int speed_index_ = 0;

    private static int speed_before_paused_ = 1;


    private void Awake() {
        Debug.Log("TimeMaster Created");
        return;
    }

    public override bool Init() {
        TogglePlay(false);
        speed_before_paused_ = 1;
        Debug.Log("TimeMaster Initialized");
        return true;
    }

    void Update() {
        if ((delta_time_ > wait_timer_ / speed_[speed_index_] && speed_index_ != 0) && GetTime() < end_date_) {
            TimeTick();
            delta_time_ = 0;
        }
        delta_time_ += Time.deltaTime;
        ChangeSpeed();
    }

    private void TimeTick() {
        if (day_ >= System.DateTime.DaysInMonth(year_, month_)) {
            if (month_ == 12) {
                year_++;
                month_ = 0;
            }
            month_++;
            day_ = 1;
        }
        else {
            day_++;
        }
        onTick.Invoke();
    }

    public static void TogglePlay(bool _play) {
        if (!_play) {
            speed_before_paused_ = speed_index_;
            speed_index_ = 0;
        }
        else {
            if(speed_before_paused_ == 0) {
                speed_before_paused_ = 1;
            }
            speed_index_ = speed_before_paused_;
        }
        onPause.Invoke();
    }

    private static void ChangeSpeed() {
        if ((Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))) {
            speed_index_++;

        }
        if (Input.GetKeyDown(KeyCode.Minus)) {
            speed_index_--;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (speed_index_ == 0) {
                TogglePlay(true);
            }
            else {
                TogglePlay(false);
            }
        }

        ClampSpeedIndex();
    }

    private static void ClampSpeedIndex() {
        speed_index_ = Mathf.Clamp(speed_index_, 0, speed_.Length - 1);
    }

    [System.Serializable]
    public class TimeTicker : UnityEngine.Events.UnityEvent {

    }

    public static void IncreaseSpeed() {
        speed_index_++;
        ClampSpeedIndex();
    }

    public static void DecreaseSpeed() {
        speed_index_--;
        ClampSpeedIndex();
    }

    public static bool IsPaused() {
        return speed_index_ == 0 ? true : false;
    }

    public static System.DateTime GetTime() {
        return new System.DateTime(year_, month_, day_);
    }
    public static int GetGameSpeed() {
        return speed_index_;
    }
}
