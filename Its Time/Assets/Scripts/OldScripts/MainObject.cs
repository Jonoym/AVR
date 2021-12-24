using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObject : MonoBehaviour
{

    public GameObject cube;

    void Start()
    {

    }
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            foreach (Transform cube in transform) {
                if (CheckCube(cube)) {
                    break;
                }
            }
            foreach (Transform cube in transform) {
                foreach (Transform child in cube) {
                    if (child.childCount != 0) {
                        Transform connector = child.GetChild(0);
                        if (connector.GetComponent<Connector>() != null && connector.GetComponent<Connector>().touching) {
                            connector.GetComponent<Connector>().OnTriggerExit(null);
                        }
                    }
                }
            }
        } 
    }

    private bool CheckCube(Transform cube) {
        foreach (Transform child in cube) {
            Debug.Log(child);
            if (child.childCount != 0) {
                Transform connector = child.GetChild(0);
                if (connector.GetComponent<Connector>() != null && connector.GetComponent<Connector>().touching) {
                    PlacePiece(connector);
                    return true;
                }   
            }
        }
        return false;
    }

    private void PlacePiece(Transform child) {
        Debug.Log(child);
        Destroy(child.GetComponent<Connector>().collided.gameObject);
        GameObject newCube = Instantiate(cube, child.parent.transform);

        newCube.transform.parent = child.transform.parent.parent.parent;
        child.GetComponent<Connector>().touching = false;

        FindObjectOfType<GenerateCube>().NewCube();

        child.GetComponent<Connector>().OnTriggerExit(null);
        Destroy(child.parent.gameObject);
    }
}
