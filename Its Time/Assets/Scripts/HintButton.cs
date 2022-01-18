using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HintButton : MonoBehaviour
{
    
    public Hint[] hints;

    public SteamVR_Action_Boolean buttonPressed;

    public Material normal;
    
    public Material hover;

    private bool hovering = false;

    private bool released = true;

    void OnTriggerEnter(Collider other) {
        if (LayerMask.NameToLayer("Lighting") == other.gameObject.layer)
        {
            GetComponent<MeshRenderer>().material = hover;
            hovering = true;    
        }
    }

    void OnTriggerExit() {
        GetComponent<MeshRenderer>().material = normal;
        hovering = false;
    }

    void Update()
    {
        if (buttonPressed.lastState && released)
        {
            released = false;
            if (hovering) {
                Debug.Log("Hint Button Pressed");
                CheckHints();
            }

        }
        else if (!buttonPressed.lastState)
        {
            released = true;
        }
    }

    void CheckHints() {
        for (int i = 0; i < hints.Length; i++) {
            for (int j = 0; j < hints[i].prerequisites.Length; j++) {
                if (!Triggered(hints[i].prerequisites[j])) {
                    DisplayHint(hints[i]);
                    return;
                }
            }
        }
    }

    bool Triggered(GameObject target) {

        if (target == null) {
            return true;
        }

        if (target.GetComponent<Bomb>() != null) {
            if (target.GetComponent<Bomb>().Exploded()) {
                return true;
            }
        }

        if (target.GetComponent<BlackholeSpawner>() != null) {
            if (target.GetComponent<BlackholeSpawner>().Spawned()) {
                return true;
            }
        }

        return false;
    }

    void DisplayHint(Hint hint) {
        if (FindObjectOfType<RotateFortress>() != null) {
            FindObjectOfType<RotateFortress>().gameObject.transform.localEulerAngles = hint.fortressRotation;
        }
        if (FindObjectOfType<Controller>() != null) {
            if (FindObjectOfType<Controller>().GetControllerPiece() != null) {
                FindObjectOfType<Controller>().GetControllerPiece().transform.eulerAngles = hint.pieceRotation;
            }
        }
    }
}
