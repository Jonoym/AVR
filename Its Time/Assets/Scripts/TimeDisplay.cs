using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{

    private TMPro.TMP_Text timeText;

    private TimeManager timeManager;

    void Start() {
        timeText = GetComponent<TMPro.TMP_Text>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    void Update() {
        timeText.text = timeManager.GetRotationTimeFormatted();
    }
}
