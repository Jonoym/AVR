using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTilt : MonoBehaviour
{

    public GameObject armCanvas;

    // Update is called once per frame
    void Update()
    {   
        if (transform.rotation.z < 0f) {
            armCanvas.SetActive(true);
        } else {
            armCanvas.SetActive(false);
        }
    }
}
