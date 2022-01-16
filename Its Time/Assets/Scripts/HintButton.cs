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
                DisplayHint();
            }

        }
        else if (!buttonPressed.lastState)
        {
            released = true;
        }
    }

    void DisplayHint() {
        if (FindObjectOfType<RotateFortress>() != null) {
            FindObjectOfType<RotateFortress>().gameObject.transform.localEulerAngles = hints[0].fortressRotation;
        }
        if (FindObjectOfType<Controller>() != null) {
            Debug.Log("here");
            if (FindObjectOfType<Controller>().GetControllerPiece() != null) {
                Debug.Log("here2");
                FindObjectOfType<Controller>().GetControllerPiece().transform.eulerAngles = hints[0].pieceRotation;
            }
        }
    }
}
