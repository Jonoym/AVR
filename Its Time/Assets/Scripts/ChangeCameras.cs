using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameras : MonoBehaviour
{

    public GameObject exteriorCamera;

    public GameObject firingCamera;

    public GameObject interiorCamera;

    public GameObject fortress;

    public GameObject fortressItem;

    private LinkedList<GameObject> items;

    public GameObject ammo;

    private int currentDisplay = 0;

    void Start()
    {
        items = new LinkedList<GameObject>();
        GetChildren(fortressItem);
        DisableRenderers();
        ChangeCamera();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeCamera();
        }
    }

    private void ChangeCamera()
    {
        if (currentDisplay == 0)
        {
            SetExteriorCamera();
        }
        else if (currentDisplay == 1)
        {
            SetInteriorCamera();
        }
        else
        {
            SetFiringCamera();
        }
        currentDisplay = (currentDisplay + 1) % 3;
    }

    private void SetExteriorCamera()
    {
        Debug.Log("Exterior");
        // Need to enable the controls for the rotation of the fortress
        exteriorCamera.SetActive(true);
        fortress.GetComponent<RotateFortress>().enabled = true;

        // Need to disable the controls for the rotation of the object
        firingCamera.SetActive(false);
        ammo.GetComponent<ControlObject>().enabled = false;

        interiorCamera.SetActive(false);
    }

    private void SetInteriorCamera()
    {
        Debug.Log("Interior");
        // Need to disable the controls for the rotation of the fortress
        exteriorCamera.SetActive(false);
        fortress.GetComponent<RotateFortress>().enabled = false;

        EnableRenderers();

        interiorCamera.SetActive(true);
    }
    private void SetFiringCamera()
    {
        Debug.Log("Firing");
        interiorCamera.SetActive(false);

        // Position the firing camera correctly 
        firingCamera.SetActive(true);
        firingCamera.transform.position = new Vector3(0, 0, -10);
        firingCamera.transform.Rotate(0, 0, 0);

        DisableRenderers();

        // Need to enable the controls for the rotation of the object
        ammo.GetComponent<ControlObject>().enabled = true;
    }

    private void GetChildren(GameObject obj) {
        if (obj == null) {
            return;
        }

        foreach (Transform child in obj.transform) {
            if (child == null) {
                continue;
            }
            items.AddLast(child.gameObject);
            GetChildren(child.gameObject);
        }
    }

    private void DisableRenderers() {
        foreach(GameObject item in items) {
            if (item == null) {
                continue;
            }

            MeshRenderer renderer = item.GetComponent<MeshRenderer>();
            if (renderer != null) {
                renderer.enabled = false;
            } else {
                ParticleSystem particle = item.GetComponent<ParticleSystem>();
                if (particle != null) {
                    particle.Stop();
                }
            }
        }
    }

    private void EnableRenderers() {
        foreach(GameObject item in items) {
            if (item == null) {
                continue;
            }

            MeshRenderer renderer = item.GetComponent<MeshRenderer>();
            if (renderer != null) {
                renderer.enabled = true;
            } else {
                ParticleSystem particle = item.GetComponent<ParticleSystem>();
                if (particle != null) {
                    particle.Play();
                }
            }
        }
    }
}
