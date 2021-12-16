using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;

    private int bossRound = 5;
    private int enemyCount; 
    public int waveNumber = 1; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber + 9); 
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; 
        // Spawn more enemies if none are left. Increase number of enemies by waves
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber + 9); 
        }

    }

    // Generate spawn positions for enemies and bosses
    private Vector3 GenerateSpawnPosition()
    {
        int random = Random.Range(1, 3); 
        if (random == 1)
        {
            // Generate x-position 
            float positiveX = Random.Range(80, 141);
            float negativeX = Random.Range(-140, -79);
            float xPosition = Random.Range(1, 3) == 1 ? positiveX : negativeX;

            // Generate z-position 
            float zPosition = Random.Range(-140, 141);

            // Generate spawn position 
            Vector3 spawnPoint = new Vector3(xPosition, 0, zPosition);
            return spawnPoint;
        }

        else
        {
            // Generate z-position
            float positiveZ = Random.Range(80, 141);
            float negativeZ = Random.Range(-140, -79);
            float zPosition = Random.Range(1, 3) == 1 ? positiveZ : negativeZ;

            // Generate x-position 
            float xPosition = Random.Range(-140, 141);

            // Generate spawn position 
            Vector3 spawnPoint = new Vector3(xPosition, 0, zPosition);
            return spawnPoint;
        }
    }

    // Spawn enemies in waves 
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation); 
        }
    }
}
