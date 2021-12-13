using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private WeaponsInventory weaponsInventory; 
    public bool isFiring = false;
    private int weaponIndex = 1; 

    // Start is called before the first frame update
    void Start()
    {
        weaponsInventory = GetComponent<WeaponsInventory>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Swap between weapons with "Q" and "E"  
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (weaponIndex == 1)
            {
                SetWeaponIndex(3); 
            }
            else weaponIndex--;

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (weaponIndex == 3)
            {
                SetWeaponIndex(1);  
            }
            else weaponIndex++;
        }

        // Fire player's weapon
        if (isFiring)
        {
            Fire(weaponIndex);
        }
    }

    // Track the weapon that the player has selected 
    private void Fire(int index)
    {
        if (index == 1)
        {
            weaponsInventory.FirePistol();
        }
        else if (index == 2)
        {
            weaponsInventory.FireMachineGun();
        }
        else if (index == 3)
        {
            weaponsInventory.FireShotgun();
        }
    }

    // Edit weapon index
    private void SetWeaponIndex(int index)
    {
        weaponIndex = index; 
    }
}
