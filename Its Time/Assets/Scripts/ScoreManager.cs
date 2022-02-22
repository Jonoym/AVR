using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int stars = 0;

    private int starsHit = 0;

    private int score = 0;

    private float scoreMultiplier = 1;

    public GameObject endScreenMenu;

    private bool displayArmMenu = true;
    
    public void IncrementStarCount() {
        stars++;
    }

    public void IncrementStarsHit() {
        starsHit++;
        score += 5000;
    }

    public int getStarsHit() {
        return starsHit;
    }

    public int getTotalStars() {
        return stars;
    }

    public int getScore() {
        return score;
    }

    public void addCrackScore() {
        score += 100;
    }

    public bool gameWon() {
        return stars == starsHit;
    }

    public void AlterMultiplier(float multiplierChange) {
        scoreMultiplier *= multiplierChange;
    }

    public void PrintScoreInfo() {
        Debug.Log("Total of " + starsHit + " stars hit out of " + stars);
        Debug.Log("Current score of " + score); 
    }

    public bool ShouldDisplayArmMenu() {
        return displayArmMenu;
    }

    private void DisableArmMenu() {
        displayArmMenu = false;
    }

    public void DisplayEndMenu() {
        DisableArmMenu();
        endScreenMenu.SetActive(true);
    }

    public int GetTimeBonus() {
        float time = FindObjectOfType<TimeManager>().GetRotationTurnTimeTotal();

        float multiplier = 50 / (time + 50);
        return (int)(5000 * multiplier);
    }
}
