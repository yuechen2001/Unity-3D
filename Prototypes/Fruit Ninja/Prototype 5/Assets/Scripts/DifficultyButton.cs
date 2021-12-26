using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    public int difficulty; 

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Run SetDifficulty method when a button is pressed 
        button.onClick.AddListener(SetDifficulty); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set difficulty of the game using buttons on start screen
    private void SetDifficulty()
    {
        gameManager.StartGame(difficulty); 
    }
}
