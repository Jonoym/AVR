using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{

    public bool timing = true;

    private TMPro.TMP_Text timeText;

    private TimeManager timeManager;

    void Start() {
        timeText = GetComponent<TMPro.TMP_Text>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    void Update() {
        if (timing) {
            timeManager.AddTime(Time.deltaTime);
        }
        timeText.text = timeManager.GetTimeFormatted();
    }
}
