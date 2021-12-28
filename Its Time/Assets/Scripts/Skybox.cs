using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time);
    }
}
