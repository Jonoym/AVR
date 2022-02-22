using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    private float damage = 0;

    public float damageRequired = 0;

    public GameObject cracked;

    public GameObject smoke;

    private bool collided = false;

    public bool destroyableByPiece = true;

    AudioManager audioManager;

    ScoreManager scoreManager;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        CheckCollisions(other);
    }

    private void CheckCollisions(Collision other) {
        if (LayerMask.NameToLayer("Piece") == other.gameObject.layer)
        {
            if (other.impulse.sqrMagnitude > 20) {
                audioManager.Play("PieceThump");
                audioManager.Play("StoneCollisions");
            }
            SetTouched();
        }
        else if (LayerMask.NameToLayer("TouchedStructure") == other.gameObject.layer)
        {
            if (other.impulse.sqrMagnitude > 20) {
                audioManager.Play("StoneCollisions");
            }
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

        if (scoreManager != null) {
            scoreManager.addCrackScore();
        }

        PlayRandomCrack();
        if (smoke != null) {
            GameObject smokeEffect = Instantiate(smoke, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), Quaternion.identity);
        }
        
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
    
    private void PlayRandomCrack() {
        int random = Random.Range(1, 6);

        audioManager.Play("StoneCrack" + random);
    }
}
