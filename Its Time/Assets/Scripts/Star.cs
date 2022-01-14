using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    public GameObject outer;

    public GameObject starHit;

    public ScoreManager scoreManager;

    void Start() {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.IncrementStarCount();
    }

    void OnTriggerEnter(Collider other) {
        GameObject particles = Instantiate(starHit, transform.position, Quaternion.identity);
        particles.transform.parent = transform.parent;

        scoreManager.IncrementStarsHit();
        Debug.Log("Star has been Hit");
        Destroy(outer);
        Destroy(gameObject);
    }
}
