using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : ManagerManager {
    private int currentMonth = 1;
    static UIManager ui_manager;

    public bool[] warnings = { false };

    private void Awake() {
        Button[] options = GameObject.Find("EventWindow").GetComponent<EventWindow>().GetComponentsInChildren<Button>();
        foreach (Button b in options) {
            b.onClick.AddListener(eventFinished);
        }

        Debug.Log("ResorceManager Created");
    }

    public override bool init() {
        ui_manager = FindObjectOfType<UIManager>();

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
        Debug.Log(Player.GetResource(Resource.Popularity));
        if (Player.GetResource(Resource.Popularity) <= 0) {
            ColorManager.ChangeControl("GAMEOVER", "SOLID");
        }
        
        ui_manager.UpdateStats();
    }

    static void checkOpinion() {
        int AliOpi, AxiOpi, SovOpi;
        AliOpi = Player.GetResource(Resource.AlliesOpinion);
        AxiOpi = Player.GetResource(Resource.AxisOpinion);
        SovOpi = Player.GetResource(Resource.SovietOpinion);
        if(AliOpi <= 0) {
            
        }
    }
}
