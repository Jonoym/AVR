using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public Transform trajectory;
    LineRenderer line;

    public int numPoints = 50;

    public float time = 0.005f;

    public LayerMask layers;

    public bool drawLine = true;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame  
    void Update()
    {
        line.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = trajectory.position;
        Vector3 startingVelocity = trajectory.forward * 20f;
        for (float t = 0.3f; t < numPoints; t += time)
        {
            if (!drawLine) {
                line.positionCount = 0;
                break;
            }
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            if (Physics.OverlapSphere(newPoint, 0.1f, layers).Length > 0)
            {
                line.positionCount = points.Count;
                break;
            }
        }

        line.SetPositions(points.ToArray());
    }
}
