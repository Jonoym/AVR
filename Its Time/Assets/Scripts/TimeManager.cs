using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private float totalTimeElapsed = 0;
    private float rotationTimeElapsed = 0;

    private float previousRotationTime = 0;

    private bool rotationTurn = false;

    void Update() {
        totalTimeElapsed += Time.deltaTime;
        if (rotationTurn) {
            rotationTimeElapsed += Time.deltaTime;
        }
    }

    public void UpdateTurn(bool status) {
        rotationTurn = status;
    }

    public void AddTime(float deltaTime) {
        rotationTimeElapsed += deltaTime;
    }

    public string GetRotationTimeFormatted() {
        return "" + ((int)rotationTimeElapsed / 60) + ":" + (((int)rotationTimeElapsed / 10) % 6) + ((int)rotationTimeElapsed % 10);
    }

    public void LogRotationTime() {
        previousRotationTime = rotationTimeElapsed;
    }

    public float GetRotationTurnTime() {
        return rotationTimeElapsed - previousRotationTime;
    }

    public float GetRotationTurnTimeTotal() {
        return rotationTimeElapsed;
    }

    public float GetTotalTimeElapsed() {
        return totalTimeElapsed;
    }
}
