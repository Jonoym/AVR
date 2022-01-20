using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    public GameObject[] items;

    private GameObject piece;

    float distance = 99999;

    GameObject closestItem;

    private bool tracking = false;

    void FixedUpdate() {
        if (piece == null) {
            piece = FindObjectOfType<Controller>().GetFiringPiece();
        }
        
        if (tracking && piece != null) {
            for (int i = 0; i < items.Length; i++) {
                CheckClosest(items[i]);
            }
        }
    }
    
    private void CheckClosest(GameObject target) {

        if (target == null) {
            return;
        }

        if (target.GetComponent<Bomb>() != null) {
            if (!target.GetComponent<Bomb>().Exploded()) {
                UpdateDistance(target);
            }
        }
        else if (target.GetComponent<BlackholeSpawner>() != null) {
            if (!target.GetComponent<BlackholeSpawner>().Spawned()) {
                UpdateDistance(target);
            }
        } else if (target.GetComponent<Star>() != null) {
            UpdateDistance(target);
        }
    }

    private void UpdateDistance(GameObject target) {
        if (piece != null) {
            if ((target.transform.position - piece.transform.position).sqrMagnitude < distance) {
                distance = (target.transform.position - piece.transform.position).sqrMagnitude;
                closestItem = target;
                Debug.Log(closestItem);
            }
        }
    }

    public void StartTracking() {
        tracking = true;
    }

    public void StopTracking() {
        tracking = false;
    }

    public void Reset() {
        closestItem = null;
        piece = FindObjectOfType<Controller>().GetFiringPiece();
    }

    public GameObject GetClosest() {
        return closestItem;
    }
}
