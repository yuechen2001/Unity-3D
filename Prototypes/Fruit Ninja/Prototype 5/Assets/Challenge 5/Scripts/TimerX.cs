using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerX : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private GameManagerX gameManager; 
    public float timeRemaining = 60;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManagerX>(); 
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }

            timerText.text = "Timer: " + Mathf.Round(timeRemaining); 

            if (Mathf.Round(timeRemaining) == 0)
            {
                gameManager.GameOver();
            }
        }
    }
}
