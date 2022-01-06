using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FireBlock : MonoBehaviour
{

    public SteamVR_Action_Boolean trigger;

    private bool fired = false;

    private GameObject firingCamera;

    private GameObject fortress;

    public float throwForce = 20f;

    private Transform leftHand;

    void Start() {
        firingCamera = FindObjectOfType<FiringCamera>().gameObject;
        fortress = FindObjectOfType<Fortress>().gameObject;
        leftHand = FindObjectOfType<LeftHand>().gameObject.transform;
    }
    void Update()
    {
        InitiateFire();
    }


    public void InitiateFire() {
        if (trigger.lastState && !fired) {
            
            fired = true;
            
            DisableControls();

            FireObject();

            if (FindObjectOfType<PieceSpawner>() != null) {
                StartCoroutine(FindObjectOfType<PieceSpawner>().CheckGameEnd(5));
            } else {
                StartCoroutine(FindObjectOfType<StartSpawner>().GetNextPiece(1));
            }
        }
    }

    public void FireObject() {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(leftHand.forward * throwForce, ForceMode.VelocityChange);
        rb.useGravity = true;
        transform.parent = fortress.transform;
    }

    private void DisableControls() {
        gameObject.GetComponent<FireBlock>().enabled = false;   

        DisableTrajectory();
        DestroyController();
    }

    private void DisableTrajectory() {
        for (int i = 0; i < transform.parent.childCount; i++) {
            Transform child = transform.parent.GetChild(i);
            Shadow shadow = child.GetComponent<Shadow>();
            if (shadow != null) {
                shadow.GetComponent<MeshRenderer>().enabled = false;
                Destroy(child.gameObject);
            }
        }
        FindObjectOfType<Trajectory>().GetComponent<LineRenderer>().enabled = false;
    }

    private void DestroyController() {
        for (int i = 0; i < transform.parent.childCount; i++) {
            Transform child = transform.parent.GetChild(i);
            ControlObject control = child.GetComponent<ControlObject>();
            if (control != null) {
                control.GetComponent<MeshRenderer>().enabled = false;
                Destroy(child.gameObject);
            }
        }
    }
}
