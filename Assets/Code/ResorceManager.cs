using UnityEngine;
using UnityEngine.UI;

public class ResorceManager : ManagerManager {

    private int currentMonth = 1;

    private void Awake() {
        Debug.Log("ResorceManager Created");
        return;
    }

    public override bool init() {
        Button[] options = FindObjectsOfType<Button>();
        foreach (Button b in options) {
            b.onClick.AddListener(eventFinished);
        }

        TimeMaster time_master = FindObjectOfType<TimeMaster>();
        time_master.onTick.AddListener(Tick);

        Debug.Log("ResorceManager Initialized");
        return true;
    }

    void Tick() {
        if(currentMonth != TimeMaster.GetTime().Month) {
            currentMonth = TimeMaster.GetTime().Month;

        }
    }




    static void eventFinished() {
        if (Player.GetResource(Resource.Popularity) <= 0) {
            ColorManager.ChangeControl("GAMEOVER", "SOLID");
        }
    }

}
