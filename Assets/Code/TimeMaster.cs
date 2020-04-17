using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMaster : MonoBehaviour {
    [Header("Events")]
    public TimeTicker onTick; // event system



    //date variables
    private static int month = 1;
    private static int day = 1;
    private static int year = 1939;

    //time tick variables
    private static float wait_timer = 3.5f;
    private static float delta_time = 0;
    private static int[] speed = { 0, 2, 4, 6, 8 };
    private static int speed_index = 0;

    private static int speedBeforePaused = 1;

    private static bool is_game_paused = false;
    private System.DateTime EndDate = new System.DateTime(1945, 5, 9);
    private void Awake() {

        TogglePlay(false);
        speedBeforePaused = 1;
    }

    void Update() {
        if ((delta_time > wait_timer / speed[speed_index] && speed_index != 0) && GetTime() < EndDate) {
            TimeTick();
            delta_time = 0;
        }
        delta_time += Time.deltaTime;
        ChangeSpeed();
    }

    private void TimeTick() {
        if (day >= System.DateTime.DaysInMonth(year, month)) {
            if (month == 12) {
                year++;
                month = 0;
            }
            month++;
            day = 1;
        }
        else {
            day++;
        }
        onTick.Invoke();
    }

    public static void TogglePlay(bool play) {
        if (!play) {
            speedBeforePaused = speed_index;
            speed_index = 0;
        }
        else {
            speed_index = speedBeforePaused;
        }
    }

    private static void ChangeSpeed() {
        if ((Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))) {
            speed_index++;
        }
        if (Input.GetKeyDown(KeyCode.Minus)) {
            speed_index--;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            is_game_paused = !is_game_paused;
            TogglePlay(is_game_paused);
        }
        Mathf.Clamp(speed_index, 0, 4);
    }

    [System.Serializable]
    public class TimeTicker : UnityEngine.Events.UnityEvent {

    }


    public static System.DateTime GetTime() {
        return new System.DateTime(year, month, day);
    }
    public static int GetGameSpeed() {
        return speed_index;
    }
}
