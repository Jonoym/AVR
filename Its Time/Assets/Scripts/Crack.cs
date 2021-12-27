using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    private float damage = 0;

    public float damageRequired = 0;

    public GameObject cracked;

    private bool collided = false;

    public bool destroyableByPiece = true;

    void OnCollisionEnter(Collision other)
    {
        CheckCollisions(other);
    }

    // void OnCollisionStay(Collision other) {
    //     CheckCollisions(other);
    // }

    private void CheckCollisions(Collision other) {
        if (LayerMask.NameToLayer("Piece") == other.gameObject.layer)
        {
            SetTouched();
        }
        else if (LayerMask.NameToLayer("TouchedStructure") == other.gameObject.layer)
        {
            SetTouched();
        }

        if (LayerMask.NameToLayer("Platform") == other.gameObject.layer)
        {
            if (gameObject.layer == LayerMask.NameToLayer("TouchedStructure"))
            {
                CrackObject();
            }
        }
        if (LayerMask.NameToLayer("TouchedStructure") == other.gameObject.layer)
        {
            Collision(other);
        }
        if (LayerMask.NameToLayer("TouchedStructure") == gameObject.layer) {
            Collision(other);
        }
    }

    private void Collision(Collision other)
    {
        if (!collided) {
            damage += other.impulse.sqrMagnitude;
            if (destroyableByPiece) {
                if (damage > damageRequired)
                {
                    CrackObject();
                }   
            } else {
                if (LayerMask.NameToLayer("Piece") != other.gameObject.layer) {
                    if (damage > damageRequired)
                    {
                        CrackObject();
                    }
                }
            }
        }
    }

    public void CrackObject()
    {
        collided = true;

        TurnOffCollision();
        
        GameObject newChild = Instantiate(cracked, transform.position, Quaternion.identity);
        newChild.transform.parent = gameObject.transform.parent.transform;
        newChild.transform.rotation = gameObject.transform.rotation;
        Destroy(gameObject);
    }

    private void TurnOffCollision() {
        MeshCollider mesh = gameObject.GetComponent<MeshCollider>();
        if (mesh != null) {
            mesh.isTrigger = true;
        }
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        if (box != null) {
            box.isTrigger = true;
        }
    }

    private void SetTouched() {
        gameObject.layer = LayerMask.NameToLayer("TouchedStructure");
    }
}
