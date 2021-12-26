using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro; 

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText; 
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pausePanel; 

    private int score;
    public float lives;
    private float spawnRate = 1.0f;
    public bool isPaused;
    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    // Update scores 
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score; 
    }

    // Update lives remaining 
    public void UpdateLives(int livesToRemove)
    {
        lives -= livesToRemove;
        livesText.text = "Lives: " + lives; 
    }

    // Start new game 
    public void StartGame(int difficulty)
    {
        // Initialise game
        isGameActive = true;
        isPaused = false; 
        score = 0;
        lives = 3;
        spawnRate /= difficulty; 

        StartCoroutine(SpawnTarget());
        livesText.text = "Lives: " + lives;
        UpdateScore(0);     

        titleScreen.gameObject.SetActive(false); 
    }

    // Allow player to pause mid game 
    public void TogglePause()
    {
        if (isGameActive)
        {
            if (!isPaused)
            {
                isPaused = true;
                pausePanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else if (isPaused)
            {
                isPaused = false;
                pausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    // Activate game over screen 
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false; 
    }

    // Restart game when restart button is pressed 
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Spawns crates and bombs while the game is ongoing 
    private IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
