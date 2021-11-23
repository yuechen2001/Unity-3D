using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs; 
    private Vector3 offset; 
    private Vector3 position = new Vector3(-2.5f, -1.761234f, 100);
    private float spawnTime = 1.1f; 
    private float timer = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // spawn a random obstacle every few seconds 
        if (timer > spawnTime)
        {
            offset = new Vector3(Random.Range(-10, 10), 0, 30);
            position = position + offset; 
            int rand = Random.Range(0, obstaclePrefabs.Count);

            GameObject obs = Instantiate(obstaclePrefabs[rand]);
            obs.transform.position = position;
            timer = 0;
            Destroy(obs, 60);
            position.x = -2.5f; 
        }

        // Destroy obstacle once player moves past the obstacle 
        timer += Time.deltaTime;
    }
}