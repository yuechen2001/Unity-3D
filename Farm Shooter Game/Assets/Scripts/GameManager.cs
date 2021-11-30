using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int score = 0; 

    // Calculate and display number of lives player has remaining 
    public void AddLives(int value)
    {
        lives += value; 
        if (lives <= 0)
        {
            Debug.Log("Game Over!");
            lives = 0;
        }
        Debug.Log("Lives = " + lives);
    }

    // Calculate and display player's score
    public void AddScore(int value)
    {
        score += value; 
        Debug.Log("Score = " + score); 
    }
}
