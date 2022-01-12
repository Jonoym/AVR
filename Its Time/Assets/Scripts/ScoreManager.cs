using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int stars = 0;

    private int starsHit = 0;

    private int score = 0;
    
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

    public void PrintScoreInfo() {
        Debug.Log("Total of " + starsHit + " stars hit out of " + stars);
        Debug.Log("Current score of " + score); 
    }
}
