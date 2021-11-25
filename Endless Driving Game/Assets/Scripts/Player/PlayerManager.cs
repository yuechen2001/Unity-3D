using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform playerTransform; 
    public static bool gameOver; 
    public GameObject gameOverPanel; 

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Trigger Game Over screen when player falls off the road 
        if (playerTransform.position.y < -20)
        {
            gameOver = true; 
        }
        // Toggle Game Over panel 
        if (gameOver)
        {
            Time.timeScale = 0; 
            gameOverPanel.SetActive(true); 
        }
    }
}
