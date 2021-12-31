using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    private AudioManager audioManager; 
    private EnemySpawner enemySpawner;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        enemySpawner = GameObject.FindWithTag("Spawn Manager").GetComponent<EnemySpawner>();
    }

    private void Update()
    {
        waveText.text = "Final Score: " + enemySpawner.waveNumber;    
    }

    // On-click functions for Game Over screen
    public void ReplayGame()
    {
        SceneManager.LoadScene("PlayState");
        audioManager.PlaySound("Button Click");
    }

    public void QuitGame()
    {
        Application.Quit();
        audioManager.PlaySound("Button Click");
    }
}
