using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelChange : MonoBehaviour
{

    public GameObject explosion;

    public float radius;

    public float force;
    
    public bool isPiece = false;
    
    public string levelName;

    void OnCollisionEnter(Collision other) {
        Explode();
    }

    private void Explode() {
        Instantiate(explosion, transform.position, transform.rotation);

        FindObjectOfType<AudioManager>().Play("BombExplosion");

        CrackNearby();

        ExplodeNearby();

        StartCoroutine(ChangeSceneDelay(1));
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

    public IEnumerator ChangeSceneDelay(float time) {
        yield return new WaitForSeconds(time);

        FindObjectOfType<SceneChanger>().ChangeScene(levelName);

    }
}
