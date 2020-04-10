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
    private static float wait_timer = 4.0f;
    private static float delta_time = 0;
    private static int speed = 0;
    private static int speedBeforePaused = 0;
    
    void Update() {
        if (delta_time > wait_timer / speed && speed != 0) {
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
            speedBeforePaused = speed;
            speed = 0;
        }
        else {
            speed = speedBeforePaused;
        }
    }

    private static void ChangeSpeed() {
        if ((Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))) {
            speed++;
        }
        if (Input.GetKeyDown(KeyCode.Minus)) {
            speed--;
        }
        Mathf.Clamp(speed, 0, 5);
    }

    [System.Serializable]
    public class TimeTicker : UnityEngine.Events.UnityEvent {

    }


    public static System.DateTime GetTime() {
        return new System.DateTime(year, month, day);
    }
    public static int GetGameSpeed() {
        return speed;
    }
}
