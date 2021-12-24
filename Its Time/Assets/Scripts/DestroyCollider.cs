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
            }
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(-force * 0.1f, transform.position, 10);
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
                rb.AddExplosionForce(-force * 0.5f, transform.position, radius);
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
