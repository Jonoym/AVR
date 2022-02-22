using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{

    private GameObject currentPiece;

    void Update() {
        GameObject piece = FindObjectOfType<Controller>().GetFiringPiece();
        if (piece != null) {
            currentPiece = piece;
        }
        transform.rotation = currentPiece.transform.rotation;
    }

}
