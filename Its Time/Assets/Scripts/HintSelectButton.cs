using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HintSelectButton : MonoBehaviour
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
                Debug.Log("Hint Select Button Pressed");
                DisplayPowerUpMenu();
            }

        }
        else if (!buttonPressed.lastState)
        {
            released = true;
        }
    }

    public void DisplayPowerUpMenu() {
        OnTriggerExit();
        FindObjectOfType<MenuDisplay>().DisplayHintMenu();
    }
    
    public void SetReleasedFalse() {
        released = false;
    }
}
