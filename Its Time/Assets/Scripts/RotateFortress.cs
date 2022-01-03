using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RotateFortress : MonoBehaviour
{
    
    public SteamVR_Action_Vector2 input;
    
    public float rotationSpeed = 30;

    private bool left;

    private bool right;

    void Awake() {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime * input.axis.x, 0);
    }
}
