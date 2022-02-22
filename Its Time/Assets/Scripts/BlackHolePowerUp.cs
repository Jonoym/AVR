using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BlackHolePowerUp : MonoBehaviour
{

    public SteamVR_Action_Boolean buttonPressed;

    public Material normal;
    
    public Material hover;

    private bool hovering = false;

    private bool released = true;

    public GameObject blackHole;

    public GameObject explosion;

    public float radius;

    public float force;

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
                Debug.Log("Black Hole Power Up Button Pressed");
                AddPowerUp();
            }

        }
        else if (!buttonPressed.lastState)
        {
            released = true;
        }
    }

    private void AddPowerUp(){
        if (FindObjectOfType<Controller>().GetFiringPiece() != null) {
            GameObject piece = FindObjectOfType<Controller>().GetFiringPiece();
            if (piece.GetComponent<BlackholeSpawner>() == null) {
                BlackholeSpawner blackHole = piece.AddComponent<BlackholeSpawner>();
                blackHole.explosion = explosion;
                blackHole.blackHole = this.blackHole;
                blackHole.radius = radius;
                blackHole.force = force;
                blackHole.isPiece = true;

                Debug.Log("Black Hole has been added to the current piece");
            }
        }
        OnTriggerExit();
        FindObjectOfType<MenuDisplay>().DisplayDefaultMenu();
    }
}