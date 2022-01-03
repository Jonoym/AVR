using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Controller : MonoBehaviour
{
    public SteamVR_Action_Boolean trigger;

    public GameObject exteriorPlayer;

    public GameObject interiorPlayer;

    public GameObject firingPlayer;

    public GameObject fortress;

    public GameObject fortressItem;

    private LinkedList<GameObject> items;

    private int currentPlayer = 0;

    private bool released = true;

    void Start()
    {
        // items = new LinkedList<GameObject>();

        // GetChildren(fortressItem);
        // DisableRenderers();
        // SwitchView();
    }
    void Update()
    {
        if (trigger.lastState && released)
        {
            SwitchView();
            released = false;
        }
        else if (!trigger.lastState)
        {
            released = true;
        }
    }

    void SwitchView()
    {
        if (currentPlayer % 3 == 0)
        {
            SetExteriorPlayer();
        }
        else if (currentPlayer % 3 == 1)
        {
            SetInteriorPlayer();
        } else
        {
            SetFiringPlayer();
        }
        currentPlayer = (currentPlayer + 1) % 3;
    }

    private void SetExteriorPlayer()
    {
        Debug.Log("Exterior");
        // Need to enable the controls for the rotation of the fortress
        exteriorPlayer.SetActive(true);
        //fortress.GetComponent<RotateFortress>().enabled = true;

        // Need to disable the controls for the rotation of the object
        firingPlayer.SetActive(false);
        // ammo.GetComponent<ControlObject>().enabled = false;
        AlterPieceControl(false);

        interiorPlayer.SetActive(false);
    }

    private void SetInteriorPlayer()
    {
        Debug.Log("Interior");
        // Need to disable the controls for the rotation of the fortress
        exteriorPlayer.SetActive(false);
        //fortress.GetComponent<RotateFortress>().enabled = false;

        EnableRenderers();

        interiorPlayer.SetActive(true);
    }
    private void SetFiringPlayer()
    {
        Debug.Log("Firing");
        interiorPlayer.SetActive(false);

        // Position the firing camera correctly 
        firingPlayer.SetActive(true);
        firingPlayer.transform.position = new Vector3(0, 0, -10);
        firingPlayer.transform.Rotate(0, 0, 0);

        DisableRenderers();

        // Need to enable the controls for the rotation of the object
        // ammo.GetComponent<ControlObject>().enabled = true;
        AlterPieceControl(true);
    }

    private void GetChildren(GameObject obj)
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
            items.AddLast(child.gameObject);
            GetChildren(child.gameObject);
        }
    }

    private void DisableRenderers()
    {
        if (items == null) {
            return;
        }
        foreach (GameObject item in items)
        {
            if (item == null)
            {
                continue;
            }

            MeshRenderer renderer = item.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
            else
            {
                ParticleSystem particle = item.GetComponent<ParticleSystem>();
                if (particle != null)
                {
                    particle.Stop();
                }
            }
        }
    }

    private void EnableRenderers()
    {
        if (items == null) {
            return;
        }
        foreach (GameObject item in items)
        {
            if (item == null)
            {
                continue;
            }

            MeshRenderer renderer = item.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }
            else
            {
                ParticleSystem particle = item.GetComponent<ParticleSystem>();
                if (particle != null)
                {
                    particle.Play();
                }
            }
        }
    }

    private void AlterPieceControl(bool set)
    {
        for (int i = 0; i < firingPlayer.transform.childCount; i++)
        {
            Transform child = firingPlayer.transform.GetChild(i);
            ControlObject control = child.GetComponent<ControlObject>();
            if (control != null)
            {
                control.enabled = set;
            }
        }
    }

    public GameObject GetFiringPiece()
    {
        for (int i = 0; i < firingPlayer.transform.childCount; i++)
        {
            Transform child = firingPlayer.transform.GetChild(i);
            ControlObject control = child.GetComponent<ControlObject>();
            if (control != null)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}

