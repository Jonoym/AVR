using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    public GameObject outer;

    public GameObject starHit;

    private ScoreManager scoreManager;

    void Start() {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.IncrementStarCount();
    }

    void OnTriggerEnter(Collider other) {
        if (LayerMask.NameToLayer("Lighting") == other.gameObject.layer)
        {
            return;  
        }

        scoreManager.IncrementStarsHit();
        
        GameObject particles = Instantiate(starHit, transform.position, Quaternion.identity);
        particles.transform.parent = transform.parent;

        Debug.Log("Star has been Hit");
        Destroy(outer);
        Destroy(gameObject);
    }
}
