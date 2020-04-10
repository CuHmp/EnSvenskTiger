using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTester : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        TimeMaster processor = FindObjectOfType<TimeMaster>();
        processor.onTick.AddListener(Tick);

    }

    void Tick() {
        Debug.Log(TimeMaster.GetTime());
    }

    // Update is called once per frame
    void Update() {

    }
}
