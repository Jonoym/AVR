using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float spinSpeed = 20;
    void FixedUpdate() {
        transform.Rotate(-spinSpeed * Time.deltaTime, -spinSpeed * Time.deltaTime, -spinSpeed * Time.deltaTime);
    }
}
