using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ExtraMovePowerUp : MonoBehaviour
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
                Debug.Log("Extra Move Button Pressed");
                AddPowerUp();
            }

        }
        else if (!buttonPressed.lastState)
        {
            released = true;
        }
    }

    private void AddPowerUp(){
        FindObjectOfType<PieceSpawner>().ExtraMove();
    }
}