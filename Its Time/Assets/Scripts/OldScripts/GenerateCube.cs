using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCube : MonoBehaviour
{

    public GameObject currentCube;
    public void NewCube() {
        Instantiate(currentCube, transform.position, Quaternion.identity);
    }
}
