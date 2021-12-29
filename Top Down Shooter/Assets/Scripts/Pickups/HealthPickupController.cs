using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    private PlayerHealthManager playerHealth;
    private AudioManager audioManager; 

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealthManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If player collides with health pickup, increase player health 
    private void OnTriggerEnter(Collider other)
    {
        // If collider is not player, stop function 
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        // Increase player health 
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.RecoverHealth();
            audioManager.PlaySound("Heal"); 
            Destroy(gameObject); 
        }
    }
}
