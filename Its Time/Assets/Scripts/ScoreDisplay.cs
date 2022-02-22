using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{

    private TMPro.TMP_Text starText;

    private ScoreManager scoreManager;

    public bool includeTime = false;
    void Start()
    {
        starText = GetComponent<TMPro.TMP_Text>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (includeTime) {
            starText.text = "" + (scoreManager.getScore() + scoreManager.GetTimeBonus());
        } else {
            starText.text = "" + scoreManager.getScore();
        }
    }
}
