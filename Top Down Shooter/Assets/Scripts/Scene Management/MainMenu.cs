using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject instructionsPanel;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        // Exit instructions panel upon button press
        if (Input.GetKeyDown(KeyCode.Q) && instructionsPanel.activeInHierarchy)
        {
            instructionsPanel.gameObject.SetActive(false); 
        }
    }

    // Start game
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayState");
        audioManager.PlaySound("Button Click");
    }

    // Show instructions panel 
    public void ReadInstructions()
    {
        instructionsPanel.gameObject.SetActive(true);
        audioManager.PlaySound("Button Click");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
