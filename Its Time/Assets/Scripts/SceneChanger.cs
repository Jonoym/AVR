using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string levelName) {

        Debug.Log("Scene Changed to " + levelName);

        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    public void RestartScene() {

        Debug.Log("Current Scene has been Restarted");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void Exit() {
        Application.Quit();
    }
}
