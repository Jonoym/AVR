using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{

    private bool timing = true;

    private float timeElapsed = 0;

    private TMPro.TMP_Text timeText;

    void Start() {
        timeText = GetComponent<TMPro.TMP_Text>();
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
