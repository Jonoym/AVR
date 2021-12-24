using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (1 << LayerMask.NameToLayer("Piece") == 1 << other.gameObject.layer)
        {
            SetTouched();
        }
        else if (1 << LayerMask.NameToLayer("TouchedStructure") == 1 << other.gameObject.layer)
        {
            SetTouched();
        }
    }

    private void SetTouched() {
        gameObject.layer = LayerMask.NameToLayer("TouchedStructure");
    }
}
