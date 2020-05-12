using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerManager : MonoBehaviour {
    static List<ManagerManager> managers_ = new List<ManagerManager>();

    private void Awake() {
        CreateManagers();
    }

    void CreateManagers() {
        AddManagers();
        InitManagers();
    }

    private void AddManagers() {
        managers_.Add(gameObject.AddComponent<TimeMaster>());
        managers_.Add(gameObject.AddComponent<ResourceManager>());
        managers_.Add(gameObject.AddComponent<ColorManager>());
        managers_.Add(gameObject.AddComponent<EventManager>());
    }

    private void InitManagers() {
        for (int i = 0; i < managers_.Count; i++) {
            if (!managers_[i].Init()) {
                Debug.LogError("Failed to initilize a manager", managers_[i]);
            }
        }
    }

    private void Start() {
        
    }
    public virtual bool Init() { return false; }

}
