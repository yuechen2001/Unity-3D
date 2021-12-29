using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    private GunController gunController;
    private WeaponsInventory weaponsInventory;

    // Start is called before the first frame update
    void Start()
    {
        gunController = GameObject.FindWithTag("Gun").GetComponent<GunController>();
        weaponsInventory = GameObject.FindWithTag("Gun").GetComponent<WeaponsInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayRemainingAmmo();
    }

    // Display remaining ammo of each weapon depending on which weapon is selected
    private void DisplayRemainingAmmo()
    {
        if (gunController.weaponIndex == 1)
        {
            ammoText.text = "Pistol Ammo: UNLIMITED";
        }
        else if (gunController.weaponIndex == 2)
        {
            ammoText.text = "Machine Gun Ammo: " + weaponsInventory.machineGunAmmo; 
        }
        else if (gunController.weaponIndex == 3)
        {
            ammoText.text = "Shotgun Ammo: " + weaponsInventory.shotgunAmmo;
        }
    }
}
