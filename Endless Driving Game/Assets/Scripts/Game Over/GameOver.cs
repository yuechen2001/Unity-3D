using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // On-click functions for Game Over screen
    public void ReplayGame()
    {
        SceneManager.LoadScene("PlayState");
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
