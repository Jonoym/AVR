using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MenuButton : MonoBehaviour
{

    public SteamVR_Action_Boolean buttonPressed;

    public Material normal;
    
    public Material hover;

    public bool hovering = false;

    public bool released = true;

    void OnTriggerEnter() {
        Debug.Log("Menu Button Hovered");
        GetComponent<MeshRenderer>().material = hover;
        hovering = true;
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
                Debug.Log("Menu Button Pressed");
                StartCoroutine(MainMenu(1));
            }

        }
        else if (!buttonPressed.lastState)
        {
            released = true;
        }
    }

    public IEnumerator MainMenu(float time) {
        yield return new WaitForSeconds(time);

        FindObjectOfType<SceneChanger>().ChangeScene("Start");
    }
}
