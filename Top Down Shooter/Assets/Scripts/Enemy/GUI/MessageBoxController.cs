using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageBoxController : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    public TextMeshProUGUI displayBox;

    private float displayTime = 3.0f;
    private float timer = 0;
    private bool isResetTimer; 


    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.FindWithTag("Spawn Manager").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (displayBox.gameObject.activeInHierarchy)
        {
            // Reset timer if new display pops up 
            if (isResetTimer)
            {
                timer = 0;
                isResetTimer = false; 
            }

            timer += Time.deltaTime; 
            if (timer >= displayTime)
            {
                displayBox.gameObject.SetActive(false);
                timer = 0; 
            }
        }
    }

    // Display wave number before the start of every wave 
    public void DisplayWaveNumber()
    {
        isResetTimer = true; 
        displayBox.gameObject.SetActive(true);
        displayBox.color = new Color(255, 125, 0, 255);
        displayBox.text = "Wave " + enemySpawner.waveNumber;
        if (enemySpawner.waveNumber % enemySpawner.bossRound == 0)
        {
            displayBox.text = "Wave " + enemySpawner.waveNumber + ", Boss Incoming!"; 
        }
    }

    // Display ammo refill when player picks up ammo boxes
    public void DisplayAmmoRefill(string ammoType)
    {
        isResetTimer = true;
        displayBox.gameObject.SetActive(true);
        displayBox.color = new Color(1, 1, 1, 1);
        displayBox.text = ammoType + " refilled!"; 
    }

    // Display power up obtained when player picks up power up 
    public void DisplayPowerupObtained(string powerupType)
    {
        isResetTimer = true;
        displayBox.gameObject.SetActive(true);
        displayBox.color = new Color(0, 50, 100, 255);
        displayBox.text = powerupType + " power-up obtained!"; 
    }
}
