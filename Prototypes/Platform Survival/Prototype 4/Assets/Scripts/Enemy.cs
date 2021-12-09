using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    public SpawnManager spawnManager; 

    private float speed = 3.0f;
    public bool isBoss = false;
    private float spawnInterval = 3.0f;
    private float nextSpawn;

    public int miniEnemyCount; 

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        spawnManager = FindObjectOfType<SpawnManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Make enemy follow player 
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed); 

        // If enemy is a boss, spawn mini enemies at regular time internals 
        if (isBoss)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemyCount); 
            }
        }

        // Destroy enemy after it falls off the platform 
        if (transform.position.y < -20)
        {
            Destroy(gameObject); 
        }
    }
}
