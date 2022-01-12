using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTilt : MonoBehaviour
{

    public GameObject armCanvas;

    void Update()
    {   
        float angle = transform.localEulerAngles.z;
        if (angle < 320f && angle > 240f) {
            armCanvas.SetActive(true);
        } else {
            armCanvas.SetActive(false);
        }
    }
}
