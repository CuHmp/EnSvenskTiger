using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerManager : MonoBehaviour {
    static List<ManagerManager> managers = new List<ManagerManager>();
    static bool init_managers = false;
    private void Awake() {
        createManagers();
    }

    void createManagers() {
        AddManagers();
        for(int i = 0; i < 100000; i++) {

        }
        initManagers();
    }

    private void AddManagers() {
        managers.Add(gameObject.AddComponent<TimeMaster>());
        managers.Add(gameObject.AddComponent<ResorceManager>());
        managers.Add(gameObject.AddComponent<ColorManager>());
    }

    private void initManagers() {
        for (int i = 0; i < managers.Count; i++) {
            if (!managers[i].init()) {
                Debug.LogError("Failed to initilize a manager", managers[i]);
            }
        }
    }


    public virtual bool init() { return false; }

}
