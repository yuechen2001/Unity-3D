using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter (Collider other)
    {
        // Check that object collided with is player 
        if (other.gameObject.name != "Player")
        {
            // if object not player, stop the function
            return;
        }
        FindObjectOfType<AudioManager>().PlaySound("Pick Up Coin");
        // Add to score 
        PlayerController.numberofCoins += 1; 
        // Destroy Coin
        Destroy(gameObject);
    }

}
