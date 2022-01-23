using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hint
{

    public Vector3 pieceRotation;
    
    public Vector3 fortressRotation;

    public GameObject[] prerequisites;

    public Vector3 direction;

    public float force;
}
