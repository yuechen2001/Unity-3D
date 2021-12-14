using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private int maxHealth = 10; 
    private int currentHealth;
    
    private Renderer rend;
    private Color storedColour;

    private float flashLength = 0.25f; 
    private float flashTimer;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponent<Renderer>();
        storedColour = rend.material.GetColor("_Color"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false); 
        }

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
        flashTimer = flashLength;
        rend.material.SetColor("_Color", Color.white);
    }
}
