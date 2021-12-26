using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private AudioSource music;
    public Slider volumeController;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPaused)
        {
            music.UnPause();
            // Adjust BGM with slider
            music.volume = volumeController.value;
        }
        else if (gameManager.isPaused)
        {
            // Stop BGM if game is paused 
            music.Pause();
        }
    }
}

