using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FireBlock : MonoBehaviour
{

    public SteamVR_Action_Vector2 fireForce;

    public SteamVR_Action_Boolean trigger;

    private bool fired = false;

    private GameObject firingPlayer;

    private GameObject fortress;

    public float throwForce = 20f;

    private Transform leftHand;

    private static bool fireAttempted = false;

    private static bool forceChangeAttemped = false;

    private bool locked;

    public Vector3 direction;

    void Start() {
        firingPlayer = FindObjectOfType<FiringPlayer>().gameObject;
        fortress = FindObjectOfType<Fortress>().gameObject;
        leftHand = FindObjectOfType<LeftHand>().gameObject.transform;
    }
    void Update()
    {
        if (!fireAttempted) {
            ControllerButtonHints.ShowButtonHint(Player.instance.rightHand, trigger);
            ControllerButtonHints.ShowTextHint(Player.instance.rightHand, trigger, "Fire");
        }
        if (!forceChangeAttemped) {
            ControllerButtonHints.ShowButtonHint(Player.instance.leftHand, fireForce);
            ControllerButtonHints.ShowTextHint(Player.instance.leftHand, fireForce, "Adjust Force");
        }

        InitiateFire();

        UpdateForce();
    }

    private void HideHints() {
        ControllerButtonHints.HideButtonHint(Player.instance.rightHand, trigger);
        ControllerButtonHints.HideTextHint(Player.instance.rightHand, trigger);
        ControllerButtonHints.HideButtonHint(Player.instance.leftHand, fireForce);
        ControllerButtonHints.HideTextHint(Player.instance.leftHand, fireForce);
    }

    private void UpdateForce() {
        if (fireForce.axis.x != 0 && fireForce.axis.y != 0 && !locked) {
            throwForce = fireForce.axis.y * 10 + 20f;
            forceChangeAttemped = true;
            ControllerButtonHints.HideButtonHint(Player.instance.leftHand, fireForce);
            ControllerButtonHints.HideTextHint(Player.instance.leftHand, fireForce);
        }
    }

    public void SetLockStatus(bool status) {
        locked = status;
    }

    public void InitiateFire() {
        if (trigger.lastState && !fired) {

            fired = true;

            fireAttempted = true;

            DistanceTracker tracker = FindObjectOfType<DistanceTracker>();
            if (tracker != null) {
                tracker.Reset();
                tracker.StartTracking();
            }

            DisablePieceControls();

            FireObject();

            PrintFireInfo();

            FindObjectOfType<LeftHand>().SetLockStatus(false);

            if (FindObjectOfType<PieceSpawner>() != null) {
                StartCoroutine(FindObjectOfType<PieceSpawner>().CheckGameEnd(5));
            } else {
                StartCoroutine(FindObjectOfType<StartSpawner>().GetNextPiece(1));
            }

            HideHints();
        }
    }

    public void FireObject() {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (locked && direction != null) {
            rb.AddForce(direction * throwForce, ForceMode.VelocityChange);
        } else {
            rb.AddForce(leftHand.forward * throwForce, ForceMode.VelocityChange);
        }
        rb.useGravity = true;
        transform.parent = fortress.transform;
    }

    private void DisablePieceControls() {
        gameObject.GetComponent<FireBlock>().enabled = false;   

        DisableTrajectory();
        DestroyController();
    }

    private void DisableTrajectory() {
        for (int i = 0; i < transform.parent.childCount; i++) {
            Transform child = transform.parent.GetChild(i);
            Shadow shadow = child.GetComponent<Shadow>();
            if (shadow != null) {
                shadow.GetComponent<MeshRenderer>().enabled = false;
                Destroy(child.gameObject);
            }
        }
        FindObjectOfType<Trajectory>().GetComponent<LineRenderer>().enabled = false;
    }

    private void DestroyController() {
        for (int i = 0; i < transform.parent.childCount; i++) {
            Transform child = transform.parent.GetChild(i);
            ControlObject control = child.GetComponent<ControlObject>();
            if (control != null) {
                control.GetComponent<MeshRenderer>().enabled = false;
                Destroy(child.gameObject);
            }
        }
    }

    private void PrintFireInfo() {
        TimeManager timer = FindObjectOfType<TimeManager>();
        if (timer == null) {
            return;
        }

        Debug.Log("Current Piece has been fired");
        Debug.Log("    Rotation: (" + transform.localEulerAngles + ")");
        if (FindObjectOfType<LeftHand>().IsLocked()) {
            Vector3 lockDirection = FindObjectOfType<LeftHand>().GetDirection();
            Debug.Log("    Direction: (" + direction.x + ", " + direction.y + ", " + direction.z + ")");
        } else {
            Debug.Log("    Direction: (" + leftHand.forward.x + ", " + leftHand.forward.y + ", " + leftHand.forward.z + ")");
        }
        Debug.Log("    Throw Force: " + throwForce);
        Debug.Log("    Exterior Angle: (" + FindObjectOfType<RotateFortress>().gameObject.transform.localEulerAngles + ")");

        Debug.Log("    Time taken for Current Rotation Turn: " + timer.GetRotationTurnTime());
        timer.LogRotationTime();
        Debug.Log("    Total Game Time: " + timer.GetTotalTimeElapsed());
        Debug.Log("    Total Rotating Time: " + timer.GetRotationTurnTimeTotal());

    }
}
