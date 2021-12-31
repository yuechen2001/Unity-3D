using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    private GameManager gameManager; 

    // Slider stats 
    public Slider playerHealthBar; 
    private int maxHealth = 10;
    private int currentHealth; 
    private int damageTaken;
    private int healthRecovered = 2;

    // Player take damage 
    private Color storedColour;
    private Renderer rend;
    private AudioManager audioManager;
    public ParticleSystem bloodSplurt;

    private float flashLength = 0.25f; 
    private float flashTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>(); 

        // Slider to show player health
        playerHealthBar.maxValue = maxHealth;
        damageTaken = 0;
        playerHealthBar.fillRect.gameObject.SetActive(false); 

        // Configure player flashing when taking damage 
        rend = GetComponent<Renderer>();
        storedColour = rend.material.GetColor("_Color"); 
    }

    // Update is called once per frame
    void Update()
    {
        // Player flashes when taking damage 
        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime; 
            if (flashTimer <= 0)
            {
                rend.material.SetColor("_Color", storedColour); 
            }
        }
    }

    // Hurt player when attacked by enemy 
    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
        HurtPlayerHealthBar(damage);
        audioManager.PlaySound("Damage");

        // Player flashes when taking damage
        flashTimer = flashLength;
        bloodSplurt.Play(); 
        rend.material.SetColor("_Color", Color.white);
    }

    // Reflect damage taken by enemy on healthbar
    public void HurtPlayerHealthBar(int amount)
    {
        damageTaken += amount;
        playerHealthBar.fillRect.gameObject.SetActive(true);
        playerHealthBar.value = damageTaken;

        if (damageTaken >= maxHealth)
        {
            gameObject.SetActive(false);
            gameManager.isGameOver = true; 
        }
    }
    
    // Increase player health if he picks up a health pickup
    public void RecoverHealth()
    {
        currentHealth += healthRecovered;
        damageTaken -= healthRecovered;
        playerHealthBar.value = damageTaken;

        // Prevent player from overhealing 
        if (currentHealth > maxHealth && damageTaken < 0)
        {
            currentHealth = maxHealth;
            damageTaken = 0;
        }

        if (currentHealth == maxHealth && damageTaken == 0)
        {
            playerHealthBar.fillRect.gameObject.SetActive(false); 
        }
    }

}
