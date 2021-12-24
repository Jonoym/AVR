using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFortress : MonoBehaviour
{

    public GameObject exteriorCamera;
    
    public float rotationSpeed = 120;

    private bool left;

    private bool right;

    void Update ()
    {
            left = Input.GetKey(KeyCode.Q);
            right = Input.GetKey(KeyCode.E);
    }

    void Awake() {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void FixedUpdate()
    {
        if (left) {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        if (right) {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
    }
}
