using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    public GameObject outer;

    public GameObject effects;

    public Material hitMaterial;

    private bool hit = false;

    public static int numStars = 0;

    public static int numHit = 0;

    void Start() {
        numStars++;
    }
    void OnTriggerEnter(Collider other) {
        if (!hit) {
            outer.GetComponent<MeshRenderer>().material = hitMaterial;

            GameObject particles = Instantiate(effects, transform.position, Quaternion.identity);
            particles.transform.parent = transform.parent;

            hit = true;
            numHit++;
        }
    }

    public static bool gameWon() {
        Debug.Log(numStars);
        Debug.Log(numHit);
        return numStars == numHit;
    }
}
