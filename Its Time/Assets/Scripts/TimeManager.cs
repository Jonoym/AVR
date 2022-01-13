using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private float timeElapsed = 0;

    public void AddTime(float deltaTime) {
        timeElapsed += deltaTime;
    }

    public string GetTimeFormatted() {
        return "" + ((int)timeElapsed / 60) + ":" + (((int)timeElapsed / 10) % 6) + ((int)timeElapsed % 10);
    }
}
