using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringCamera : MonoBehaviour
    {
    Vector3 Angles;
    
    public float sensitivityX;
    
    public float sensitivityY;

    public Transform player;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // float rotationY = Input.GetAxis("Mouse Y") * sensitivityX;
        // Angles = new Vector3(Mathf.MoveTowards(Angles.x, 90, -rotationY), 0);
        // transform.localEulerAngles = Angles;

        float rotationY = Input.GetAxis("Mouse Y") * sensitivityX;
        player.Rotate(0, Input.GetAxis("Mouse X") * sensitivityY, 0);
        Angles = new Vector3(Mathf.MoveTowards(Angles.x, 90, -rotationY),0);
        transform.localEulerAngles = Angles;
    }
}