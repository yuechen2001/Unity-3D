using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxController : MonoBehaviour
{
    private WeaponsInventory weaponsInventory; 

    // Start is called before the first frame update
    void Start()
    {
        weaponsInventory = GameObject.FindWithTag("Gun").GetComponent<WeaponsInventory>();  
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
        }
        else if (gameObject.CompareTag("Machine Gun Ammo"))
        {
            weaponsInventory.ResupplyAmmo("Machine Gun");
        }

        // Destroy ammo box after collision
        Destroy(gameObject); 
    }
}
