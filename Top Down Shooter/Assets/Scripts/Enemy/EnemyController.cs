using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyRB;
    private PlayerController player;

    private float moveSpeed = 5f;  
    private float playingAreaLimit = 170;

    // Start is called before the first frame update 
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy to follow player 
        transform.LookAt(player.transform.position);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    
        // Destroy enemy if it is knocked out of playing area 
        if (transform.position.x > playingAreaLimit || transform.position.x < -playingAreaLimit || transform.position.z > playingAreaLimit || transform.position.z < -playingAreaLimit)
        {
            Destroy(gameObject);
        }
    }
}