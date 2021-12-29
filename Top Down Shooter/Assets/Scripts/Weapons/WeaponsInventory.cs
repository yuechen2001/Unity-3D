using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInventory : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public ParticleSystem muzzleFlash;

    // Weapon stats 
    public float bulletSpeed;
    public float shotInterval;
    private float shotTimer;

    // Weapon range
    private float shotgunRange = 0.4f;
    private float pistolRange = 1;
    private float machineGunRange = 2; 

    // Weapon magazine size
    public float shotgunAmmo = 50;
    public float machineGunAmmo = 200; 

    // Number of pellets per shotgun burst
    private int pelletsPerBurst = 4; 

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
            var newBullet = SpawnBullet(); 
            StartCoroutine(RemoveBullet(pistolRange, newBullet)); 
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
                var newBullet = SpawnBullet();

                // Destroy bullet beyond weapon range. Account for ammo 
                StartCoroutine(RemoveBullet(machineGunRange, newBullet));
                machineGunAmmo--;
                Debug.Log("Rounds: " + machineGunAmmo);

            }
        }
    }

    // Switch to shotgun
    public void FireShotgun()
    {
        // Set weapon stats 
        shotInterval = 2.0f;
        bulletSpeed = 200f;

        // Allow player to fire shotgun at regular intervals, when there is ammo
        if (shotgunAmmo > 0)
        {
            if (shotTimer > shotInterval)
            {
                shotTimer = 0;
                int initialRotation = -20;
                for (int i = 0; i < pelletsPerBurst; i++)
                {
                    var newBullet = SpawnBullet();
                    newBullet.transform.Rotate(new Vector3(newBullet.transform.rotation.x, initialRotation, newBullet.transform.rotation.z));

                    // Destroy pellets beyond shot
                    StartCoroutine(RemoveBullet(shotgunRange, newBullet));
                    initialRotation += 10;
                }

                // Account for ammo 
                shotgunAmmo--;
                Debug.Log("Rounds: " + shotgunAmmo);

            }
        }
    }

    // Spawn bullet using object pooling 
    private GameObject SpawnBullet()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation; 
            bullet.SetActive(true);
            muzzleFlash.Play();
        }
        return bullet; 
    }

    // Un-spawn bullet after max range 
    public IEnumerator RemoveBullet(float range, GameObject bullet)
    {
        yield return new WaitForSeconds(range);
        bullet.SetActive(false);
    }

    // Refill ammo after picking up ammo boxes 
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
