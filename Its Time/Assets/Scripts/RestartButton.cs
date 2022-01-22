using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RestartButton : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed.lastState && released)
        {
            released = false;
            if (hovering) {
                Debug.Log("Restart Button Pressed");
                StartCoroutine(RestartLevel(1));
            }

        }
        else if (!buttonPressed.lastState)
        {
            released = true;
        }
    }

    public IEnumerator RestartLevel(float time) {
        yield return new WaitForSeconds(time);

        FindObjectOfType<SceneChanger>().RestartScene();
    }

    public void SetReleasedFalse() {
        released = false;
    }
}
