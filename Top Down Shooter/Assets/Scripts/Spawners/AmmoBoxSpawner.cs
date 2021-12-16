using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxSpawner : MonoBehaviour
{
    public GameObject[] ammoPrefabs;
    private float spawnTimer;
    private float spawnInterval = 60; 
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        int ammoBoxCount = FindObjectsOfType<AmmoBoxController>().Length; 

        // Spawn an ammo box every 60s. Limit the total number to 5
        if (spawnTimer > spawnInterval && ammoBoxCount < 5)
        {
            int random = Random.Range(0, ammoPrefabs.Length);
            Instantiate(ammoPrefabs[random], GenerateSpawnPosition(), ammoPrefabs[random].transform.rotation);
        }
    }

    // Generate spawn position for ammo boxes 
    private Vector3 GenerateSpawnPosition()
    {
        float xPosition = Random.Range(-70, 71);
        float zPosition = Random.Range(-70, 71);
        Vector3 spawnPoint = new Vector3(xPosition, 0, zPosition);
        return spawnPoint; 
    }
}
