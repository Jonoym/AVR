using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    public float defaultLength = 5.0f;
    public GameObject dot;
    private VRInput inputModule;
    private LineRenderer lineRenderer = null;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }

    private void UpdateLine() {
        float targetLength = defaultLength;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if (hit.collider != null) {
            endPosition = hit.point;
        }

        dot.transform.position = endPosition;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length) {
        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);

        return hit;
    }
}
