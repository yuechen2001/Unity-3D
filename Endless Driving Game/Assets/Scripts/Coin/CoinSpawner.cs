using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    private Vector3 offset; 
    private Vector3 position = new Vector3(1.1f, 0, 10);
    private float spawnTime = 0.5f; 
    private float timer = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn a random coin every few seconds 
        if (timer > spawnTime)
        {
            offset = new Vector3(Random.Range(-13, 17), 0, 30);
            position = position + offset; 

            GameObject obs = Instantiate(coinPrefab);
            obs.transform.position = position;
            timer = 0;
            Destroy(obs, 60);
            position.x = -2.5f; 
        }

        timer += Time.deltaTime;
    }
}