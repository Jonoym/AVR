using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollider : MonoBehaviour
{
    public float radius;

    public float force;

    void Update()
    {
        GravitationForce();

        TagNearby();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BlackHoleForce")
        {
            FindObjectOfType<AudioManager>().Play("BlackHole3");
            Destroy(other.gameObject);
        }
    }

    private void GravitationForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.tag != "BlackHoleForce")
            {
                continue;
            }
            if (LayerMask.NameToLayer("Piece") == nearbyObject.gameObject.layer)
            {
                continue;
            } else {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(-force * 0.6f, transform.position, 10);
                }
            }
        }
    }

    private void TagNearby()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (LayerMask.NameToLayer("Piece") != nearbyObject.gameObject.layer)
                {
                    rb.useGravity = false;
                    nearbyObject.gameObject.tag = "BlackHoleForce";
                }
            }
            Crack crackable = nearbyObject.GetComponent<Crack>();
            if (crackable != null)
            {
                crackable.CrackObject();
            }
        }
    }
}
