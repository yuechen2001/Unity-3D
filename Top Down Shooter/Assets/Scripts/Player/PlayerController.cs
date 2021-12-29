using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PowerUpType currentPowerup = PowerUpType.None;
    public GameObject powerupIndicator;
    public Camera mainCamera;
    private Rigidbody playerRb;
    private GunController gun;
    private MessageBoxController messageBoxController;

    // Particles 
    public ParticleSystem smashParticles;
    public ParticleSystem rocketParticles;

    // Rocket powerup 
    public GameObject rocketPrefab;
    public Transform rocketSpawnpoint; 
    private GameObject tmpRocket;

    // Smash powerup 
    private float hangTime = 1;
    private float smashSpeed = 10;
    private int smashDamage = 4; 
    private float explosionForce = 10;
    private float explosionRadius = 20;
    private bool smashing = false;
    private float floorY;
    
    // Player stats
    private float playerSpeed = 15f;
    private float playingFieldRange = 75;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); 
        gun = GameObject.FindWithTag("Gun").GetComponent<GunController>();
        messageBoxController = GameObject.FindWithTag("Display Manager").GetComponent<MessageBoxController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement. Prevent player from walking off the map 
        RestrictPlayingArea();
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * playerSpeed * Time.deltaTime);
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * playerSpeed * Time.deltaTime);

        // Using the mouse as an aiming point 
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength; 

        // Set player to look in the direction of the mouse
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
        }

        // Player can fire with left mouse-button. Releasing the button stops the firing
        if (Input.GetMouseButtonDown(0))
        {
            gun.isFiring = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gun.isFiring = false; 
        }

        // If player has powerup, allow player to use powerup upon button press
        if (currentPowerup == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            rocketParticles.Play(); 
            LaunchRockets();
        }

        if (currentPowerup == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
    }

    // Consume powerup if player collides with it 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            currentPowerup = other.gameObject.GetComponent<PowerUp>().powerUpType;
            messageBoxController.DisplayPowerupObtained(currentPowerup.ToString());
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true); 
        }
    }

    // Prevent player from moving off the platform 
    private void RestrictPlayingArea()
    {
        if (transform.position.x > playingFieldRange)
        {
            transform.position = new Vector3(playingFieldRange, transform.position.y, transform.position.z); 
        }
        if (transform.position.x < -playingFieldRange)
        {
            transform.position = new Vector3(-playingFieldRange, transform.position.y, transform.position.z); 
        }
        if (transform.position.z > playingFieldRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, playingFieldRange);
        }
        if (transform.position.z < -playingFieldRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -playingFieldRange);
        }
    }

    // Reset player's powerup status 
    private void RemovePowerupStatus()
    {
        currentPowerup = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false); 
    }

    // Player launches homing rockets at enemies and boss
    private void LaunchRockets()
    {
        // Fire at enemies 
        foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            tmpRocket = Instantiate(rocketPrefab, rocketSpawnpoint.position, rocketPrefab.transform.rotation);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
        
        // Fire at boss
        if (GameObject.FindGameObjectWithTag("Boss") != null )
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position, rocketPrefab.transform.rotation);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(GameObject.FindGameObjectWithTag("Boss").transform);
        }

        RemovePowerupStatus(); 
    }

    IEnumerator Smash()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
        var boss = GameObject.FindGameObjectWithTag("Boss");

        // Store y position before Player takes off
        floorY = transform.position.y;

        // Calculate the amount of time it takes for Player to go up
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            // Move the player up, while maintain x velocity 
            playerRb.velocity = new Vector3(playerRb.velocity.x, smashSpeed, 0);
            yield return null;
        }

        while (transform.position.y > floorY)
        {
            // Move the player back down to original position 
            playerRb.velocity = new Vector3(playerRb.velocity.x, -smashSpeed * 2, 0);
            yield return null;
        }

        smashParticles.Play();

        for (int i = 0; i < enemies.Length; i++)
        {
            // Apply an explosion force that originates from player position. Deal damage only if enemy/boss within explosiveRadius
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
                float range = Vector3.Distance(transform.position, enemies[i].transform.position); 
                if (range < explosionRadius)
                {
                    enemies[i].GetComponent<EnemyHealthManager>().HurtEnemy(smashDamage);
                }
            }
        }

        if (boss != null)
        {
            boss.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            float range = Vector3.Distance(transform.position, boss.transform.position);
            if (range < explosionRadius)
            {
                boss.GetComponent<EnemyHealthManager>().HurtEnemy(smashDamage);
            }
        }

        // Set smashing to false 
        smashing = false;
        RemovePowerupStatus(); 
    }
}


