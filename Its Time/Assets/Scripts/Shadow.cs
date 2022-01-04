using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{

    private GameObject currentPiece;

    void Update() {
        Debug.Log("Shadow " + transform.rotation);
        
        GameObject piece = FindObjectOfType<Controller>().GetFiringPiece();
        if (piece != null) {
            currentPiece = piece;
        }
        transform.rotation = currentPiece.transform.rotation;
    }

}
