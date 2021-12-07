using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private float speed = 30; 
    private float leftBound = -15;  
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Move GameObject to the left, only when game is not over
        if (playerControllerScript.gameOver == false)
            {
                if (playerControllerScript.doubleSpeed == true)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * (speed * 2)); 
                }
                else
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed); 
                }
            }

        // Destroy obstacle if it moves past left bound 
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject); 
        }
    }
}
