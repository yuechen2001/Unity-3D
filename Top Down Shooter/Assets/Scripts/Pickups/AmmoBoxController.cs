using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxController : MonoBehaviour
{
    private WeaponsInventory weaponsInventory;
    private MessageBoxController messageBoxController;
    private AudioManager audioManager; 

    // Start is called before the first frame update
    void Start()
    {
        weaponsInventory = GameObject.FindWithTag("Gun").GetComponent<WeaponsInventory>();
        messageBoxController = FindObjectOfType<MessageBoxController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If player collides with ammo box, destroy ammo box and refill ammo 
    private void OnTriggerEnter(Collider other)
    {
        // If collider is not player, stop function 
        if (!other.gameObject.CompareTag("Player"))
        {
            return; 
        }

        // Refill ammo based on ammo box type
        if (gameObject.CompareTag("Shotgun Ammo"))
        {
            weaponsInventory.ResupplyAmmo("Shotgun");
            messageBoxController.DisplayAmmoRefill(gameObject.tag);
            audioManager.PlaySound("Pickup");
        }
        else if (gameObject.CompareTag("Machine Gun Ammo"))
        {
            weaponsInventory.ResupplyAmmo("Machine Gun");
            messageBoxController.DisplayAmmoRefill(gameObject.tag);
            audioManager.PlaySound("Pickup");
        }

        // Destroy ammo box after collision
        Destroy(gameObject); 
    }
}
