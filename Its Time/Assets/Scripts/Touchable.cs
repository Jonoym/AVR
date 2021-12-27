using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : MonoBehaviour
{

    private bool touched = false;
    void OnCollisionEnter(Collision other)
    {
        if (LayerMask.NameToLayer("Piece") == other.gameObject.layer)
        {
            SetTouched();
        }
        else if (LayerMask.NameToLayer("TouchedStructure") == other.gameObject.layer)
        {
            SetTouched();
        }
    }

    private void SetTouched() {
        if (!touched) {
            Debug.Log("Touched");
            FindObjectOfType<AudioManager>().Play("Stone 1");
            touched = true;
            gameObject.layer = LayerMask.NameToLayer("TouchedStructure");
        }
    }
}
