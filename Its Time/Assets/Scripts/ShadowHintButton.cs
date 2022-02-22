using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ShadowHintButton : MonoBehaviour
{

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
                Debug.Log("Shadow Hint Button Pressed");
                DisplayShadow();
            }

        }
        else if (!buttonPressed.lastState)
        {
            released = true;
        }
    }

    public void DisplayShadow() {
        FindObjectOfType<PieceSpawner>().SpawnShadow();

        OnTriggerExit();
        FindObjectOfType<MenuDisplay>().DisplayDefaultMenu();
    }
}
