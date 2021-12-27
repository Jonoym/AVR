using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSound : MonoBehaviour
{


    private AudioManager manager;

    void Start() {
        manager = FindObjectOfType<AudioManager>();
    }
    void OnCollisionEnter(Collision other) {
        if (other.impulse.sqrMagnitude > 20) {
            manager.Play("PieceThump");
            manager.Play("StoneCollisions");
        }
    }
}
