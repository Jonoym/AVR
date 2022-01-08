using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public Transform trajectoryPoint;
    LineRenderer line;

    public int numPoints = 100;

    public float time = 0.005f;

    public LayerMask layers;

    public bool drawLine = true;

    private GameObject shadow;

    private Controller cameras;

    private Transform leftHand;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        if (transform.GetComponent<ChangeCameras>() == null) {
            cameras = transform.GetComponent<Controller>();
        }
        // else 
        // {
        //     cameras = transform.GetComponent<ChangeCameras>();
        // }
    }

    // Update is called once per frame  
    void Update()
    {
        if (leftHand == null) {
            if (FindObjectOfType<LeftHand>().gameObject.transform != null) {
                leftHand = FindObjectOfType<LeftHand>().gameObject.transform;
            }
        } else if (FindObjectOfType<FiringCamera>().enabled) {
            line.positionCount = numPoints;
            List<Vector3> points = new List<Vector3>();
            Vector3 startingPosition = trajectoryPoint.position;
            Vector3 startingVelocity = leftHand.forward * 20f;
            for (float t = 0.25f; t < numPoints; t += time)
            {
                if (!drawLine) {
                    line.positionCount = 0;
                    break;
                }
                Vector3 newPoint = startingPosition + t * startingVelocity;
                newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
                points.Add(newPoint);

                if (Physics.OverlapSphere(newPoint, 0.8f, layers).Length > 0)
                {
                    line.positionCount = points.Count;
                    if (shadow != null)  {
                        shadow.transform.position = newPoint;
                        // if (cameras.GetFiringPiece() != null) {
                        //     Transform ammoTransform = cameras.GetFiringPiece().transform;
                        //     shadow.transform.rotation = ammoTransform.rotation;

                        // }
                    }
                    break;
                }
            }

            line.SetPositions(points.ToArray());
        }

    }

    public void SetShadow(GameObject shadow) {
        Debug.Log(shadow);
        this.shadow = shadow;
    }
}
