using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score;

    public void UpdateScore(int amountToIncrease)
    {
        score += amountToIncrease;
        Debug.Log("Current Score: " + score);
    }
}
