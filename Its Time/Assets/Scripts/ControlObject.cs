using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObject : MonoBehaviour
{

    private GameObject firingCamera;

    public float rotationSpeed = 90;

    private bool xAxis;

    private bool yAxis;

    private bool zAxis;

    // Start is called before the first frame update
    void Start() {
        firingCamera = FindObjectOfType<FiringCamera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent = firingCamera.transform;
        // if (firingCamera.activeSelf)
        // {
        //     xAxis = Input.GetKey(KeyCode.Q);
        //     yAxis = Input.GetKey(KeyCode.W);
        //     zAxis = Input.GetKey(KeyCode.E);
        // }
    }

    void FixedUpdate()
    {
        // if (xAxis)
        // {
        //     transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
        // }
        // if (yAxis)
        // {
        //     transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        // }
        // if (zAxis)
        // {
        //     transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        // }
    }
}
