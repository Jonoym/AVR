using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    private int currentPiece = 0;

    public GameObject[] controllerPieces;

    public GameObject[] pieces;

    public GameObject[] shadows;

    public GameObject trajectory;

    public GameObject controllerSpawn;

    public bool controlsEnabled = true;

    public void Awake() {
        NextTurn();
    }

    public IEnumerator CheckGameEnd(float time) {

        TimeManager timer = FindObjectOfType<TimeManager>();

        controlsEnabled = false;
        timer.UpdateTurn(false);

        yield return new WaitForSeconds(time);

        controlsEnabled = true;
        timer.UpdateTurn(true);

        FindObjectOfType<ScoreManager>().PrintScoreInfo();

        if (!FindObjectOfType<ScoreManager>().gameWon()){
            if (pieces.Length > currentPiece) {
                NextTurn();
            } else {
                Debug.Log("Level Failed");
                FindObjectOfType<SceneChanger>().ChangeScene("Start");
            }
        } else {
            Debug.Log("Level Completed");
            FindObjectOfType<SceneChanger>().ChangeScene("Start");
        }

    }

    private void NextTurn() {
        SpawnPiece();

        SpawnControlPiece();

        SpawnShadow();

        currentPiece++;

        Debug.Log("New Piece has Spawned");
        Debug.Log("Current Piece Number is " + currentPiece);
    }

    private void SpawnPiece() {
        GameObject piece = Instantiate(pieces[currentPiece], trajectory.transform.position, Quaternion.identity);
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
        GameObject piece = Instantiate(controllerPieces[currentPiece], controllerSpawn.transform.position, Quaternion.identity);

        piece.SetActive(true);

        piece.transform.parent = transform.parent;
        piece.transform.Rotate(new Vector3(0, 45, 0));

        Rigidbody rb = piece.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void SpawnShadow() {
        GameObject shadow = Instantiate(shadows[currentPiece], trajectory.transform.position, Quaternion.identity);

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
