using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTilt : MonoBehaviour
{

    public GameObject armCanvas;

    public GameObject queue;

    void Update()
    {   
        float angle = transform.localEulerAngles.z;
        if (FindObjectOfType<ScoreManager>().ShouldDisplayArmMenu()) {
            if (angle < 320f && angle > 240f) {
                armCanvas.SetActive(true);
                queue.GetComponent<QueueDisplay>().EnableRenderers();
            } else {
                armCanvas.SetActive(false);
                queue.GetComponent<QueueDisplay>().DisableRenderers();
            }
        } else {
            armCanvas.SetActive(false);
            queue.GetComponent<QueueDisplay>().DisableRenderers();
        }
    }
}
