using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    public GameObject outer;

    public GameObject starHit;

    public static int numStars = 0;

    public static int numHit = 0;

    void Start() {
        numStars++;
    }
    void OnTriggerEnter(Collider other) {
        GameObject particles = Instantiate(starHit, transform.position, Quaternion.identity);
        particles.transform.parent = transform.parent;

        Destroy(outer);
        numHit++;
        Destroy(gameObject);
    }

    public static bool gameWon() {
        Debug.Log(numStars);
        Debug.Log(numHit);
        return numStars == numHit;
    }
}
