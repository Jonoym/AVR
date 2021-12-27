using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlock : MonoBehaviour
{

    private static int currentPiece = 1;
    
    public GameObject firingCamera;

    public static GameObject[] pieces;

    public GameObject newPiece;

    public GameObject fortress;

    public float throwForce = 20f;

    private Transform oldTransform;

    public GameObject cameras;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            
            DisableControls();

            FireObject();

            StartCoroutine(CheckGameEnd(5));
        }
    }

    private void FireObject() {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(firingCamera.transform.forward * throwForce, ForceMode.VelocityChange);
        rb.useGravity = true;
        oldTransform = transform.parent;
        transform.parent = fortress.transform;
    }

    private void DisableControls() {
        gameObject.GetComponent<FireBlock>().enabled = false;   
        gameObject.GetComponent<ControlObject>().enabled = false;

        DisableTrajectory();
    }

    private void SpawnPiece() {
        
        GameObject piece = Instantiate(pieces[currentPiece++], oldTransform.position + new Vector3(0, 0, 4), Quaternion.identity);

        EnableTrajectory();

        piece.transform.parent = oldTransform;
        piece.transform.Rotate(new Vector3(0, 45, 0));
        
        piece.GetComponent<FireBlock>().enabled = true;   
        piece.GetComponent<ControlObject>().enabled = true;

        Rigidbody rb = piece.GetComponent<Rigidbody>();
        if (piece.GetComponent<BoxCollider>() != null) {
            piece.GetComponent<BoxCollider>().enabled = true;
        } else if (piece.GetComponent<MeshCollider>() != null) {
            piece.GetComponent<MeshCollider>().enabled = true;
        }
        piece.GetComponent<MeshRenderer>().enabled = true;
        rb.useGravity = false;
    }

    IEnumerator CheckGameEnd(float time) {
        yield return new WaitForSeconds(time);

        if (!Star.gameWon()){
            if (currentPiece >= pieces.Length) {
                SpawnPiece();
            } else {
                Debug.Log("LEVEL FAILED");
            }
        } else {
            Debug.Log("LEVEL COMPLETE");
        }

    }

    private void DisableTrajectory() {
        for (int i = 0; i < transform.parent.childCount; i++) {
            Transform child = transform.parent.GetChild(i);
            Shadow shadow = child.GetComponent<Shadow>();
            if (shadow != null) {
                shadow.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        cameras.GetComponent<LineRenderer>().enabled = false;
    }

    private void EnableTrajectory() {
        for (int i = 0; i < transform.parent.childCount; i++) {
            Transform child = transform.parent.GetChild(i);
            Shadow shadow = child.GetComponent<Shadow>();
            if (shadow != null) {
                shadow.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        cameras.GetComponent<LineRenderer>().enabled = true;
    }
}
