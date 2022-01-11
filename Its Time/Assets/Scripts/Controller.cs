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

    private GameObject exteriorRotationPoint;

    public GameObject fortressItem;

    private LinkedList<GameObject> items;

    private int currentPlayer = 2;

    private bool released = true;

    void Start()
    {
        items = new LinkedList<GameObject>();

        GetChildren(fortressItem);

        exteriorRotationPoint = FindObjectOfType<RotateFortress>().gameObject;
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
        if (FindObjectOfType<PieceSpawner>() != null)
        {
            if (!FindObjectOfType<PieceSpawner>().controlsEnabled)
            {
                return;
            }
        }

        if (currentPlayer % 3 == 0)
        {
            SetFiringPlayer();
        }
        else if (currentPlayer % 3 == 1)
        {
            SetInteriorPlayer();
        }
        else
        {
            SetExteriorPlayer();
        }
        currentPlayer = (currentPlayer + 1) % 3;
    }

    private void SetExteriorPlayer()
    {
        Debug.Log("Exterior");
        // Need to enable the controls for the rotation of the fortress

        exteriorRotationPoint.GetComponent<RotateFortress>().enabled = true;
        // Need to disable the controls for the rotation of the object
        firingPlayer.SetActive(false);
        interiorPlayer.SetActive(false);

        exteriorPlayer.SetActive(true);

        AlterPieceControl(false);
        DisableRenderers();

        interiorPlayer.SetActive(false);
    }

    private void SetInteriorPlayer()
    {
        Debug.Log("Interior");
        // Need to disable the controls for the rotation of the fortress
        exteriorPlayer.SetActive(false);
        firingPlayer.SetActive(false);
        exteriorRotationPoint.GetComponent<RotateFortress>().enabled = false;


        AlterPieceControl(false);
        EnableRenderers();

        interiorPlayer.SetActive(true);
    }
    private void SetFiringPlayer()
    {
        Debug.Log("Firing");
        interiorPlayer.SetActive(false);
        exteriorPlayer.SetActive(false);
        exteriorRotationPoint.GetComponent<RotateFortress>().enabled = false;


        // Position the firing camera correctly 
        firingPlayer.SetActive(true);

        DisableRenderers();

        // Need to enable the controls for the rotation of the object
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
        if (items == null)
        {
            return;
        }
        foreach (GameObject item in items)
        {
            if (item == null)
            {
                continue;
            }

            MeshRenderer renderer = item.GetComponent<MeshRenderer>();
            ParticleSystem particle = item.GetComponent<ParticleSystem>();
            Light light = item.GetComponent<Light>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
            else if (particle != null)
            {
                particle.Stop();
            }
            else if (light != null) 
            {
                light.enabled = false;
            }
        }
    }

    private void EnableRenderers()
    {
        if (items == null)
        {
            return;
        }
        foreach (GameObject item in items)
        {
            if (item == null)
            {
                continue;
            }

            MeshRenderer renderer = item.GetComponent<MeshRenderer>();
            ParticleSystem particle = item.GetComponent<ParticleSystem>();
            Light light = item.GetComponent<Light>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }
            else if (particle != null)
            {
                particle.Play();
            }
            else if (light != null) 
            {
                light.enabled = true;
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
            FireBlock control = child.GetComponent<FireBlock>();
            if (control != null)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}

