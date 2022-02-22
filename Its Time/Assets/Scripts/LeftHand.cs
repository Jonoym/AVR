using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{

    private bool locked;

    private Vector3 direction;

    public void SetLockStatus(bool status) {
        locked = status;
    }
    
    public bool IsLocked() {
        return locked;
    }

    public void SetDirection(Vector3 direction) {
        this.direction = direction;
    }

    public Vector3 GetDirection() {
        return direction;
    }

}
