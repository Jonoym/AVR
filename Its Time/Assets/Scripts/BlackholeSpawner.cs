using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSpawner : MonoBehaviour
{

    public GameObject blackHole;

    public GameObject explosion;

    public float radius;

    public float force;

    private bool spawned = false;

    public bool isPiece = false;

    void OnCollisionEnter(Collision other)
    {
        if (LayerMask.NameToLayer("Lighting") == other.gameObject.layer)
        {
            return;  
        }

        if (!spawned) {
            spawned = true;
            SpawnBlackhole();
        }
    }

    private void SpawnBlackhole()
    {
        Instantiate(explosion, transform.position, transform.rotation);

        FindObjectOfType<AudioManager>().Play("BlackHole");
        FindObjectOfType<AudioManager>().Play("BlackHole2");

        CrackNearby();

        TagNearby();

        GameObject newBlackHole = Instantiate(blackHole, transform.position, Quaternion.identity);
        newBlackHole.transform.parent = gameObject.transform.parent;

        if (isPiece) {
            Debug.Log("Piece Black Hole has Exploded");
        } else {
            Debug.Log("Black Hole has been hit");
            Destroy(gameObject);
        }

    }

    private void CrackNearby() {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, radius + 1);
        foreach (Collider nearbyObject in nearbyObjects)
        {
            Crack crackable = nearbyObject.GetComponent<Crack>();
            if (crackable != null)
            {
                crackable.CrackObject();
            }
        }
    }

    private void TagNearby() {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in nearbyObjects)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (LayerMask.NameToLayer("Piece") != nearbyObject.gameObject.layer)
                {
                    rb.useGravity = false;
                    nearbyObject.gameObject.tag = "BlackHoleForce";
                }
                else
                {
                    nearbyObject.GetComponent<MeshRenderer>().enabled = false;
                    nearbyObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }

    public bool Spawned() {
        return spawned;
    }
}
