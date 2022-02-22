using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatusDisplay : MonoBehaviour
{

    private TMPro.TMP_Text statusText;

    private ScoreManager scoreManager;
    void Start()
    {
        statusText = GetComponent<TMPro.TMP_Text>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (scoreManager.gameWon()) {
            statusText.text = "LEVEL COMPLETED";
        } else {
            statusText.text = "LEVEL INCOMPLETE";
        }
    }
}
