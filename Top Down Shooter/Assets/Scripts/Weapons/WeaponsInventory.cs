using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInventory : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;

    // Weapon stats 
    public float bulletSpeed;
    public float shotInterval;
    private float shotTimer;

    // Weapon range
    private float shotgunRange = 0.2f;
    private float pistolRange = 1;
    private float machineGunRange = 2; 

    // Weapon magazine size
    private float shotgunAmmo = 50;
    private float machineGunAmmo = 200; 

    // Number of pellets per shotgun burst
    private int pelletsPerBurst = 6; 

    // Start is called before the first frame update
    void Start()
    {
        shotTimer = shotInterval;
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;
    }

    // Switch to pistol 
    public void FirePistol()
    {
        // Set weapon stats 
        shotInterval = 0.5f;
        bulletSpeed = 100f;

        // Allow player to fire pistol at regular intervals 
        if (shotTimer > shotInterval)
        {
            shotTimer = 0;
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Destroy(newBullet, pistolRange);
        }
    }

    // Switch to machine gun
    public void FireMachineGun()
    {
        // Set weapon stats
        shotInterval = 0.1f;
        bulletSpeed = 150f;

        // Allow player to fire MG at regular intervals, when there is ammo 
        if (machineGunAmmo > 0)
        {
            if (shotTimer > shotInterval)
            {
                shotTimer = 0;
                GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

                // Destroy bullet beyond weapon range. Account for ammo 
                Destroy(newBullet, machineGunRange);
                machineGunAmmo--;
                Debug.Log("Rounds: " + machineGunAmmo);

            }
        }
    }

    // Switch to shotgun
    public void FireShotgun()
    {
        // Set weapon stats 
        shotInterval = 1.0f;
        bulletSpeed = 200f;

        // Allow player to fire shotgun at regular intervals, when there is ammo
        if (shotgunAmmo > 0)
        {
            if (shotTimer > shotInterval)
            {
                shotTimer = 0;
                int initialRotation = -45;
                for (int i = 0; i < pelletsPerBurst; i++)
                {
                    GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.Rotate(new Vector3(newBullet.transform.rotation.x, initialRotation, newBullet.transform.rotation.z));
                    
                    // Destroy pellets beyond shot
                    Destroy(newBullet, shotgunRange);
                    initialRotation += 15;
                }

                // Account for ammo 
                shotgunAmmo--;
                Debug.Log("Rounds: " + shotgunAmmo);

            }
        }
    }

    // Method to refill ammo after picking up ammo boxes 
    public void ResupplyAmmo(string ammoType)
    {
        Debug.Log(ammoType + " ammo refilled!"); 
        if (ammoType == "Shotgun")
        {
            shotgunAmmo = 50; 
        }
        else if (ammoType == "Machine Gun")
        {
            machineGunAmmo = 200; 
        }
    }    
}