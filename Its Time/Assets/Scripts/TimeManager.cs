using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public bool timing = true;

    private float timeElapsed = 0;

    public TMPro.TMP_Text timeText;

    void Start() {
    }

    public void enableTimer() {
        timing = true;
    }

    public void disableTimer() {
        timing = false;
    }

    void Update() {
        if (timing) {
            timeElapsed += Time.deltaTime;
            timeText.text = "" + ((int)timeElapsed / 60) + ":" + (((int)timeElapsed / 10) % 6) + ((int)timeElapsed % 10);
        }
    }
}
