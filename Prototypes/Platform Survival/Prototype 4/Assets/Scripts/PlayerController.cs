using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PowerUpType currentPowerup = PowerUpType.None; 

    private Rigidbody playerRb;
    private GameObject focalPoint;

    public GameObject powerupIndicator;
    private Coroutine powerupCountdown;

    // Rockets powerup 
    public GameObject rocketPrefab;
    private GameObject tmpRocket;

    // Smash powerup 
    private float hangTime = 1;
    private float smashSpeed = 10;
    private float explosionForce = 100;
    private float explosionRadius = 20;
    private bool smashing = false;
    private float floorY; 

    private bool hasPowerUp = false;
    private float speed = 5.0f;
    private float powerUpStrength = 15.0f; 

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        // Apply force on the ball in the direction the camera is pointing in
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        // Have the indicator follow the player 
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        // If player has rockets powerup, fire rockets when "F" is pressed 
        if (currentPowerup == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets(); 
        }

        // If player has smash powerup, activate smash when "Space" is pressed 
        if (currentPowerup == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash()); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Consume powerup if player collides with it. Display powerup indicator 
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            currentPowerup = other.gameObject.GetComponent<PowerUp>().powerUpType; 
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true); 

            // If player already has a powerup, restart countdown 
            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine()); 
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player collides with enemy and has powerup Pushback, instantly push the enemy away from the player 
        if (collision.gameObject.CompareTag("Enemy") && currentPowerup == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

            Debug.Log("Collided with " + collision.gameObject.name + " with Powerup set to " + currentPowerup.ToString());
        }
    }

    private void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform); 
        }
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();

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

        // Cycle through all enemies 
        for (int i = 0; i < enemies.Length; i++)
        {
            // Apply an explosion force that originates from player position 
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }

            // Set smashing to false 
            smashing = false; 
            powerupIndicator.SetActive(false);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        // Remove powerup after 7 seconds, and remove powerup indicator 
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        currentPowerup = PowerUpType.None; 
        powerupIndicator.gameObject.SetActive(false);
    }
}
