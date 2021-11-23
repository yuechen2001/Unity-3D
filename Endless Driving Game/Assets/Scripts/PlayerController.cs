    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public SpawnManager spawnManager;
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
        // ROtates the car based on horizontal input 
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

        score.text = "Score: " + numberofCoins; 
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
