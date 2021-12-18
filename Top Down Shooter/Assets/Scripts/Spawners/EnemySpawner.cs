using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;

    private int bossRound = 5;
    private int enemyCount; 
    private int waveNumber = 4;

    private float bossYOffset = 5;
    private float enemyYOffset = 2; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber );
        SpawnBoss(); 
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Boss").Length;
        // Spawn more enemies if none are left. Increase number of enemies by waves
        if (enemyCount == 0)
        {
            waveNumber++;

            // Spawn a boss every x number of waves 
            if (waveNumber % bossRound == 0)
            {
                SpawnBoss();
                SpawnEnemyWave(waveNumber + 9); 
            }
            else
            {
                SpawnEnemyWave(waveNumber + 9);
            }
        }
    }

    // Generate spawn positions for enemies and bosses
    private Vector3 GenerateSpawnPosition(bool isBoss)
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
            if (isBoss)
            {
                Vector3 spawnPoint = new Vector3(xPosition, bossYOffset, zPosition);
                return spawnPoint;
            }
            else
            {
                Vector3 spawnPoint = new Vector3(xPosition, enemyYOffset, zPosition);
                return spawnPoint;
            }
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
            if (isBoss)
            {
                Vector3 spawnPoint = new Vector3(xPosition, bossYOffset, zPosition);
                return spawnPoint;
            }
            else
            {
                Vector3 spawnPoint = new Vector3(xPosition, enemyYOffset, zPosition);
                return spawnPoint;
            }
        }
    }

        // Spawn enemies in waves 
        private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(false), enemyPrefabs[randomEnemy].transform.rotation); 
        }
    }

    // Spawn boss wave 
    private void SpawnBoss()
    {
        Instantiate(bossPrefab, GenerateSpawnPosition(true), bossPrefab.transform.rotation); 
    }
}
