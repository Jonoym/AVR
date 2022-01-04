using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RotateFortress : MonoBehaviour
{
    
    public SteamVR_Action_Vector2 input;
    
    public float rotationSpeed = 30;

    void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime * -input.axis.x, 0);
    }
}
