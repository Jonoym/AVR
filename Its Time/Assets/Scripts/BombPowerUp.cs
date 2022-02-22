using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BombPowerUp : MonoBehaviour
{

    public SteamVR_Action_Boolean buttonPressed;

    public Material normal;
    
    public Material hover;

    private bool hovering = false;

    private bool released = true;

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
                Debug.Log("Bomb Power Up Button Pressed");
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
            if (piece.GetComponent<Bomb>() == null) {
                Bomb bomb = piece.AddComponent<Bomb>();
                bomb.explosion = explosion;
                bomb.radius = radius;
                bomb.force = force;
                bomb.isPiece = true;

                Debug.Log("Bomb has been added to the current piece");
            }
        }
        OnTriggerExit();
        FindObjectOfType<MenuDisplay>().DisplayDefaultMenu();
    }
}