using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawner : MonoBehaviour
{
    private int currentPiece = 0;

    public GameObject controllerPiece;

    public GameObject pieceSelect;

    public GameObject shadowSelect;

    public void Start() {
        NextTurn();
    }

    public IEnumerator GetNextPiece(float time) {
        yield return new WaitForSeconds(time);

        NextTurn();
    }

    private void NextTurn() {
        SpawnPiece();

        SpawnControlPiece();

        SpawnShadow();

        currentPiece++;
    }

    private void SpawnPiece() {
        GameObject piece = Instantiate(pieceSelect, transform.position + new Vector3(0, 0f, 2f), Quaternion.identity);

        piece.SetActive(true);

        EnableTrajectory();

        piece.transform.parent = transform.parent;
        piece.transform.Rotate(new Vector3(0, 45, 0));

        
        piece.GetComponent<FireBlock>().enabled = true;   

        Rigidbody rb = piece.GetComponent<Rigidbody>();
        if (piece.GetComponent<BoxCollider>() != null) {
            piece.GetComponent<BoxCollider>().enabled = true;
        } else if (piece.GetComponent<MeshCollider>() != null) {
            piece.GetComponent<MeshCollider>().enabled = true;
        }
        piece.GetComponent<MeshRenderer>().enabled = true;
        rb.useGravity = false;
    }

    private void SpawnControlPiece() { 
        GameObject piece = Instantiate(controllerPiece, transform.position + new Vector3(0, 1f, 0.7f), Quaternion.identity);

        piece.SetActive(true);

        piece.transform.parent = transform.parent;
        piece.transform.Rotate(new Vector3(0, 45, 0));

        Rigidbody rb = piece.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void SpawnShadow() {
        GameObject shadow = Instantiate(shadowSelect, transform.position + new Vector3(0, 0f, 3f), Quaternion.identity);

        shadow.SetActive(true);

        shadow.transform.parent = transform.parent;

        FindObjectOfType<Trajectory>().SetShadow(shadow);
    }

    private void EnableTrajectory() {
        for (int i = 0; i < transform.parent.childCount; i++) {
            Transform child = transform.parent.GetChild(i);
            Shadow shadow = child.GetComponent<Shadow>();
            if (shadow != null) {
                shadow.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        FindObjectOfType<Trajectory>().GetComponent<LineRenderer>().enabled = true;
    }
}
