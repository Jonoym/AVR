using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RotateFortress : MonoBehaviour
{
    
    public SteamVR_Action_Vector2 fortressRotation;
    
    public float rotationSpeed = 30;

    private static bool rotateHint = false;

    private static bool hintsTurnedOff = false;

    private GameObject exteriorPlayer;

    private LinkedList<GameObject> hands;

    void Start() {
        hands = new LinkedList<GameObject>();

        exteriorPlayer = FindObjectOfType<ExteriorPlayer>().gameObject;

        GetHands(exteriorPlayer);
    }
    
    void FixedUpdate()
    {
        if (hands.Count == 0) {
            GetHands(exteriorPlayer);
        }
        transform.Rotate(0, rotationSpeed * Time.deltaTime * -fortressRotation.axis.x, 0);
        if (fortressRotation.axis.x != 0 && fortressRotation.axis.y != 0) {
            rotateHint = true;
            DisableHands();
        } else {
            EnableHands();
        }
        if (!rotateHint) {
            ShowHint();
        } else {
            if (!hintsTurnedOff) {
                HideHints();
                hintsTurnedOff = true;
            }
        }
    }

    private void ShowHint() {
        ControllerButtonHints.ShowButtonHint(Player.instance.leftHand, fortressRotation);
        ControllerButtonHints.ShowTextHint(Player.instance.leftHand, fortressRotation, "Rotate View");
    }

    private void HideHints() {
        ControllerButtonHints.HideButtonHint(Player.instance.rightHand, fortressRotation);
        ControllerButtonHints.HideTextHint(Player.instance.rightHand, fortressRotation);
    }

    private void GetHands(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        foreach (Transform child in obj.transform)
        {
            if (child == null)
            {
                continue;
            }
            if (child.gameObject.GetComponent<RenderModel>() != null) {
                hands.AddLast(child.gameObject);
            }
            GetHands(child.gameObject);
        }
    }

    private void EnableHands() {
        foreach (GameObject hand in hands) {
            hand.SetActive(true);
        }
    }

    private void DisableHands() {
        foreach (GameObject hand in hands) {
            hand.SetActive(false);
        }
    }
}
