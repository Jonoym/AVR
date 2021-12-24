using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovePiece : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
    }
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0)) {
        //     Debug.Log("Pressed primary button.");
        //     RaycastHit hitInfo = new RaycastHit();
        //     bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        //     if (hit) {
        //         Debug.Log("Hit " + hitInfo.transform.gameObject.name);
        //         if (hitInfo.transform.gameObject.tag == "Block")
        //         {
        //             Debug.Log ("It's working!");
        //         } else {
        //             Debug.Log ("nopz");
        //         }
        //     } else {
        //         Debug.Log("No hit");
        //     }
        // }

    } 
}
