using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab; 
    private float spawnRange = 9.0f;
    private int enemyCount;
    private int waveNumber = 1; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup(); 
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; 

        // Spawn more enemies if none are left. Increase number of enemies by waves
        if (enemyCount == 0)
        {
            waveNumber++; 
            SpawnEnemyWave(waveNumber);
            SpawnPowerup(); 
        }
    }

    // Randomly generate spawn position of enemy 
    private Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(spawnRange, -spawnRange);
        float zPos = Random.Range(spawnRange, -spawnRange);
        Vector3 spawnPoint = new Vector3(xPos, 0, zPos);
        return spawnPoint; 
    }

    // Spawn enemies in waves
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Spawn a powerup 
    private void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);  
    }
}
