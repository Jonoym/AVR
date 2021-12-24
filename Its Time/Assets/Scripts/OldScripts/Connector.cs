using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{

    public GameObject indicator;
    
    public Material material;

    public Material triggeredMaterial;

    public GameObject collided = null;

    public bool touching = false;

    public void OnTriggerEnter(Collider other) {
        Debug.Log("Enter");
        if (other.gameObject.tag == "Block") {
            indicator.GetComponent<MeshRenderer>().material = triggeredMaterial;
            collided = other.gameObject;
            touching = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        Debug.Log("Exit");
        indicator.GetComponent<MeshRenderer>().material = material;
        collided = null;
        touching = false;
    }
}
