using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonusDisplay : MonoBehaviour
{

    private TMPro.TMP_Text bonusText;

    private ScoreManager scoreManager;

    void Start() {
        bonusText = GetComponent<TMPro.TMP_Text>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update() {
        bonusText.text = "" + scoreManager.GetTimeBonus();
    }
}
