using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject explosion;

    public float radius;

    public float force;
    
    public bool isPiece = false;

    void OnCollisionEnter(Collision other) {
        if (LayerMask.NameToLayer("Piece") == other.gameObject.layer)
        {
            Explode();
        }
        if (LayerMask.NameToLayer("TouchedStructure") == other.gameObject.layer)
        {
            Explode();
        }
        Explode();
    }

    private void Explode() {
        Instantiate(explosion, transform.position, transform.rotation);

        FindObjectOfType<AudioManager>().Play("BombExplosion");

        CrackNearby();

        ExplodeNearby();

        Destroy(gameObject);
    }

    private void CrackNearby() {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in nearbyObjects) {
            Crack crackable = nearbyObject.GetComponent<Crack>();
            if (crackable != null) {
                crackable.CrackObject();
            }
        }
    }

    private void ExplodeNearby() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        
        foreach (Collider nearbyObject in colliders) {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        } 
    }
}
