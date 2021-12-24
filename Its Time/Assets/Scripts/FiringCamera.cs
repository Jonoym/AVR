using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringCamera : MonoBehaviour
    {
    Vector3 Angles;
    
    public float sensitivityX;
    
    public float sensitivityY;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float rotationY = Input.GetAxis("Mouse Y") * sensitivityX;
        Angles = new Vector3(Mathf.MoveTowards(Angles.x, 90, -rotationY), 0);
        transform.localEulerAngles = Angles;
    }
}