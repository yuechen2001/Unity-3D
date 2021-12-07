using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playeRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator; 
    private bool hasPowerUp = false;
    public float speed = 5.0f;
    private float powerUpStrength = 15.0f; 

    // Start is called before the first frame update
    void Start()
    {
        playeRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        // Apply force on the ball in the direction the camera is pointing in
        float forwardInput = Input.GetAxis("Vertical");
        playeRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        // Have the indicator follow the player 
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Consume powerup if player collides with it. Display powerup indicator 
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true); 
            StartCoroutine(PowerupCountdown());
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player collides with enemy and has powerup, instantly push the enemy away from the player 
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            Debug.Log("Collided with " + collision.gameObject.name + " with Powerup set to " + hasPowerUp);
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse); 
        }
    }

    IEnumerator PowerupCountdown()
    {
        // Remove powerup after 7 seconds, and remove powerup indicator 
        yield return new WaitForSeconds(7);
        hasPowerUp = false; 
        powerupIndicator.gameObject.SetActive(false);
    }
}
