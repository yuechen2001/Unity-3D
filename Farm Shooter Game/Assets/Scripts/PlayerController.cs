using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput; 
    private float verticalInput; 
    private float speed = 10.0f; 
    private float xRange = 16; 
    private float upperZRange = 4;
    private float lowerZRange = -2; 
    public GameObject projectilePrefab; 
    public Transform projectileSpawnPoint; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent player from moving off game screen 
        RestrictPlayerXMovement(xRange);
        RestrictPlayerZMovement(upperZRange, lowerZRange); 
    
        // Allow the player to move across the screen 
        horizontalInput = Input.GetAxis("Horizontal"); 
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime); 
        verticalInput = Input.GetAxis("Vertical"); 
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime); 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Launch projectile from player
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);     
        }
    }

    void RestrictPlayerXMovement(float range)
    {
        // Prevent player from moving off the game screen 
        if (transform.position.x > range)
        {
            transform.position = new Vector3(range, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -range)
        {
            transform.position = new Vector3(-range, transform.position.y, transform.position.z);
        }
    }

    void RestrictPlayerZMovement(float upperRange, float lowerRange)
    {
        // Prevent player from moving off the game screen 
        if (transform.position.z > upperRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upperRange);
        }

        if (transform.position.z < lowerRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lowerRange);
        }
    }
}
