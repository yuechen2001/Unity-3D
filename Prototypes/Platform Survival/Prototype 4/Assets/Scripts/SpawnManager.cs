using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyprefabs;
    public GameObject[] powerupPrefabs;
    public GameObject[] miniEnemiesPrefab;
    public GameObject bossPrefab;

    private int bossRound = 5; 
    private float spawnRange = 9.0f;
    private int enemyCount;
    public int waveNumber = 1; 

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
        // Spawn one powerup per wave
        if (enemyCount == 0)
        {
            waveNumber++; 
            SpawnPowerup(); 

            // Spawn a boss every x number of waves 
            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
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

    // Determine number of mini enemies to spawn each boss wave
    public void SpawnBossWave(int currentRound)
    {
        int miniEnemiesToSpawn; 

        if (bossRound != 0)
        {
            miniEnemiesToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemiesToSpawn = 1; 
        }

        GameObject boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemyCount = miniEnemiesToSpawn;
    }

    // Spawn mini enemies
    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemiesPrefab.Length);
            Instantiate(miniEnemiesPrefab[randomMini], GenerateSpawnPosition(), miniEnemiesPrefab[randomMini].transform.rotation);
        }
    }

    // Spawn enemies in waves
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyprefabs.Length);
            Instantiate(enemyprefabs[randomEnemy], GenerateSpawnPosition(), enemyprefabs[randomEnemy].transform.rotation);
        }
    }

    // Spawn a powerup 
    private void SpawnPowerup()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);  
    }
}
