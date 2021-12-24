using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlock : MonoBehaviour
{

    public int numTurns = 3;
    public GameObject firingCamera;

    public GameObject newPiece;

    public GameObject fortress;

    public float throwForce = 20f;

    private Transform oldTransform;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            
            FireObject();

            DisableControls();

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
        firingCamera.GetComponent<Trajectory>().drawLine = false;
        gameObject.GetComponent<FireBlock>().enabled = false;   
        gameObject.GetComponent<ControlObject>().enabled = false;
        Debug.Log("DISABLED");
    }

    private void SpawnPiece() {
        
        firingCamera.GetComponent<Trajectory>().drawLine = true;
        GameObject piece = Instantiate(newPiece, oldTransform.position + new Vector3(0, 0, 4), Quaternion.identity);

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
        numTurns--;
    }

    IEnumerator CheckGameEnd(float time) {
        yield return new WaitForSeconds(time);

        if (!Star.gameWon()){
            if (numTurns >= 1) {
                SpawnPiece();
            } else {
                Debug.Log("LEVEL FAILED");
            }
        } else {
            Debug.Log("LEVEL COMPLETE");
        }

    }
}
