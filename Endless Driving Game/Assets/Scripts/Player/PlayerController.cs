    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public SpawnManager spawnManager;
    public GameObject player;  
    private float speed = 20.0f; 
    private float turnSpeed = 45.0f;
    private float horizontalInput; 
    public static int numberofCoins; 
    public Text score; 
    // Start is called before the first frame update
    void Start()
    { 
        numberofCoins= 0; 
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        // Move the vehicle forward based on vertical input 
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        // Rotates the car based on horizontal input 
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        // Display player's score on top right corner
        score.text = "Score: " + numberofCoins; 

        // Trigger Game Over screen when player falls off the road 
        if (gameObject.transform.position.y > -100)
        {
            PlayerManager.gameOver = true; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnTrigger")
        {
            spawnManager.SpawnTriggerEntered();
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            FindObjectOfType<AudioManager>().PlaySound("Collision");
        }
    }
}
