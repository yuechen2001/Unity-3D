using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; 
    private PlayerController playerControllerScript; 
    private Vector3 spawnPoint = new Vector3(25, 0, 0);
    private float startDelay = 2.0f; 
    private float timeInterval = 2.0f; 

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); 
        InvokeRepeating("SpawnObstacle", startDelay, timeInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn obstacles after a time interval 
    void SpawnObstacle()
    {
        // Randomize spawning of obstacles 
        int index = Random.Range(0, obstaclePrefabs.Length);

        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefabs[index], spawnPoint, obstaclePrefabs[index].transform.rotation); 
        }
    }
}
