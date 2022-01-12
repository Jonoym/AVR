using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ControlObject : MonoBehaviour
{

    private GameObject firingCamera;

    private Controller controller;

    // Start is called before the first frame update
    void Start() {
        firingCamera = FindObjectOfType<FiringCamera>().gameObject;
        controller = FindObjectOfType<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent = firingCamera.transform;

        GameObject piece = controller.GetFiringPiece();
        if (piece != null) {
            piece.transform.rotation = transform.rotation;
        }
    }
}
