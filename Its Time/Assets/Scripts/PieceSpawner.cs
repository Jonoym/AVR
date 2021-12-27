using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    private int currentPiece = 0;

    public GameObject[] pieces;

    public GameObject[] shadows;

    public void Start() {
        NextTurn();
    }

    public IEnumerator CheckGameEnd(float time) {
        yield return new WaitForSeconds(time);

        if (!Star.gameWon()){
            if (pieces.Length > currentPiece) {
                NextTurn();
            } else {
                Debug.Log("LEVEL FAILED");
            }
        } else {
            Debug.Log("LEVEL COMPLETE");
        }

    }

    private void NextTurn() {
        SpawnPiece();

        SpawnShadow();

        currentPiece++;
    }

    private void SpawnPiece() {
        
        GameObject piece = Instantiate(pieces[currentPiece], transform.position + new Vector3(0, 0, 0), Quaternion.identity);

        Debug.Log("NEW PIECE HAS BEEN SPAWNED");
        piece.SetActive(true);

        EnableTrajectory();

        piece.transform.parent = transform.parent;
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

    private void SpawnShadow() {
        GameObject shadow = Instantiate(shadows[currentPiece], transform.position + new Vector3(0, 0, 4), Quaternion.identity);

        Debug.Log("NEW SHADOW HAS BEEN SPAWNED");
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
