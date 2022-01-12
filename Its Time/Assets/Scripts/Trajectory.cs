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

    private bool shadowSpawned = false;

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
        shadowSpawned = false;
        if (leftHand == null) {
            if (FindObjectOfType<LeftHand>().gameObject.transform != null) {
                leftHand = FindObjectOfType<LeftHand>().gameObject.transform;
            }
        } else if (FindObjectOfType<FiringPlayer>().enabled) {
            line.positionCount = numPoints;
            List<Vector3> points = new List<Vector3>();
            Vector3 startingPosition = trajectoryPoint.position;
            FireBlock fireBlock = FindObjectOfType<FireBlock>();
            if (fireBlock == null) {
                return;
            }
            Vector3 startingVelocity = leftHand.forward * fireBlock.throwForce;
            for (float t = 0.05f; t < numPoints; t += time)
            {
                if (!drawLine) {
                    line.positionCount = 0;
                    break;
                }
                Vector3 newPoint = startingPosition + t * startingVelocity;
                newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
                points.Add(newPoint);

                if (Physics.OverlapSphere(newPoint, 0.5f, layers).Length > 0)
                {
                    shadowSpawned = true;
                    line.positionCount = points.Count;
                    if (shadow != null)  {
                        shadow.transform.position = newPoint;
                        shadow.GetComponent<MeshRenderer>().enabled = true;
                    }
                    break;
                }
            }
            if (!shadowSpawned) {
                if (shadow != null)  {
                    shadow.GetComponent<MeshRenderer>().enabled = false;
                }
            }

            line.SetPositions(points.ToArray());
        }

    }

    public void SetShadow(GameObject shadow) {
        this.shadow = shadow;
    }
}
