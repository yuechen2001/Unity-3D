using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    private AudioManager audioManager; 

    public bool isPaused;
    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>(); 
        Time.timeScale = 1;
        isPaused = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle Game Over panel 
        if (isGameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        // Toggle pause panel 
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause(); 
        }
    }

    // Pause game when P is pressed. Unpause game when P is pressed
    private void TogglePause()
    {
        if (!isGameOver)
        {
            if (!isPaused)
            {
                isPaused = true;
                audioManager.PauseSound(); 
                pausePanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else if (isPaused)
            {
                isPaused = false;
                audioManager.ResumeSound(); 
                pausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
