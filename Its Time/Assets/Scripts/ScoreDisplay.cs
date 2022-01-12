using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{

    private TMPro.TMP_Text starText;

    private ScoreManager scoreManager;
    void Start()
    {
        starText = GetComponent<TMPro.TMP_Text>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        starText.text = "" + scoreManager.getScore();
    }
}
