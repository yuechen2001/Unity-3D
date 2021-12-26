using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle; 

    // Initialising object stats
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;

    // Individual object's points 
    public int pointValue; 

    void Start()
    {
        // Randomize initial position and movement of objects 
        targetRb = GetComponent<Rigidbody>(); 
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        // Reference to Game Manager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Destroy game object on swipe
    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
    
    // If player missed a crate, remove one life. If no more lives, trigger game over
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            if (gameManager.lives > 0)
            {
                gameManager.UpdateLives(1);
            }
            else if (gameManager.lives == 0)
            {
                gameManager.GameOver();
            }
        }
    }

    // Generate random forces, torques and spawn positions 
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed); 
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque); 
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos); 
    }
}
