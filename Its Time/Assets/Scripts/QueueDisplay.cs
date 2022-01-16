using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueDisplay : MonoBehaviour
{

    private bool disabled = false;
    void ClearQueue() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void UpdateQueue(GameObject[] pieces, int currentPiece) {
        ClearQueue();
        int piecesShown = pieces.Length - currentPiece > 5 ? 5 : pieces.Length - currentPiece;
        for (int i = 1; i < piecesShown; i++) {
            GameObject piece = Instantiate(pieces[i + currentPiece], transform.position, transform.rotation);
            piece.transform.parent = transform;
            piece.transform.localPosition += new Vector3((i - 1)/10f, 0, 0);
        }
        if (disabled) {
            disabled = false;
            DisableRenderers();
        } else {
            disabled = true;
            EnableRenderers();
        }
    }

    public void DisableRenderers() {
        if (!disabled) {
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            }
            disabled = true;
        }
    }

    public void EnableRenderers() {
        if (disabled) {
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            }
            disabled = false;
        }
    }
}
