using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInput : MonoBehaviour
{

    public static GameObject currentObject;
    private int currentID;

    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CreateRaycast();
    }

    void CreateRaycast() {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);

        for (int i = 0; i < hits.Length; i++) {
            RaycastHit hit = hits[i];
            int id = hit.collider.gameObject.GetInstanceID();

            if (currentID != id) {
                currentID = id;
                currentObject = hit.collider.gameObject;
                if (currentObject.name == "Next") {
                    Debug.Log("HIT NEXT");
                }

                if (currentObject.tag == "Button") {
                    Debug.Log("HIT BUTTON");
                }
            }
        }
    }
}
