using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    private int currentPiece = 0;

    private int addedPieces = 0;

    public GameObject[] controllerPieces;

    public GameObject[] pieces;

    public GameObject baseShadow;

    public GameObject[] shadows;

    public GameObject[] queuePieces;

    public GameObject trajectory;

    public GameObject controllerSpawn;

    public GameObject spawnPoint;

    private GameObject currentShadow;

    public bool controlsEnabled = true;
    
    private bool spawning = false;

    private bool firstTurn = false;

    public void Update() {
        if (spawning && !firstTurn) {
            firstTurn = true;
            NextTurn();
        }
    }

    public IEnumerator CheckGameEnd(float time) {

        TimeManager timer = FindObjectOfType<TimeManager>();

        controlsEnabled = false;
        timer.UpdateTurn(false);

        yield return new WaitForSeconds(time);

        ScoreManager score = FindObjectOfType<ScoreManager>();

        FindObjectOfType<DistanceTracker>().StopTracking();

        score.PrintScoreInfo();

        if (!score.gameWon()){
            if (pieces.Length > currentPiece) {
                controlsEnabled = true;
                timer.UpdateTurn(true);
                NextTurn();
            } else {
                Debug.Log("Level Failed");
                score.DisplayEndMenu();
                //FindObjectOfType<SceneChanger>().ChangeScene("Start");
            }
        } else {
            Debug.Log("Level Completed");
            score.DisplayEndMenu();
        }

    }

    public void SetSpawnStatus(bool status) {
        spawning = status;
    }

    private void NextTurn() {

        if (!spawning) {
            RenderQueue();
            return;
        }

        SpawnPiece();

        SpawnControlPiece();

        SpawnBaseShadow();

        currentPiece++;

        Debug.Log("New Piece has Spawned");
        Debug.Log("Current Piece Number is " + (currentPiece + addedPieces));

        RenderQueue();
    }

    private void SpawnPiece() {
        GameObject piece = Instantiate(pieces[currentPiece], trajectory.transform.position, Quaternion.identity);
        piece.SetActive(true);

        EnableTrajectory();

        piece.transform.parent = spawnPoint.transform.parent;
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

        piece.transform.parent = spawnPoint.transform.parent;
        piece.transform.Rotate(new Vector3(0, 45, 0));

        Rigidbody rb = piece.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void SpawnBaseShadow() {
        GameObject shadow = Instantiate(baseShadow, trajectory.transform.position, Quaternion.identity);

        shadow.SetActive(true);

        shadow.transform.parent = spawnPoint.transform.parent;
        FindObjectOfType<Trajectory>().SetShadow(shadow);

        currentShadow = shadow;
    }

    public void SpawnShadow() {

        if (currentShadow != null) {
            Destroy(currentShadow);
        } else {
            return;
        }
        GameObject shadow = Instantiate(shadows[currentPiece - 1], trajectory.transform.position, Quaternion.identity);

        shadow.SetActive(true);

        shadow.transform.parent = spawnPoint.transform.parent;
        FindObjectOfType<Trajectory>().SetShadow(shadow);

        currentShadow = shadow;
    }

    private void EnableTrajectory() {
        for (int i = 0; i < spawnPoint.transform.parent.childCount; i++) {
            Transform child = spawnPoint.transform.parent.GetChild(i);
            Shadow shadow = child.GetComponent<Shadow>();
            if (shadow != null) {
                shadow.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        FindObjectOfType<Trajectory>().GetComponent<LineRenderer>().enabled = true;
    }

    public void RenderQueue() {
        if (currentPiece == 0) {
            FindObjectOfType<QueueDisplay>().UpdateQueue(queuePieces, currentPiece);
        } else {
            FindObjectOfType<QueueDisplay>().UpdateQueue(queuePieces, currentPiece - 1);
        }
    }

    public void ExtraMove() {
        if (currentPiece != 0) {
            currentPiece--;
            addedPieces++;
            RenderQueue();
        }
    }
}
