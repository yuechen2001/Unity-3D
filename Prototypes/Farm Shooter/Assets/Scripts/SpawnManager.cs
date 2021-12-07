using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 20;
    private float spawnPosZ = 20; 
    private float startDelay = 2; 
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval); 
        InvokeRepeating("SpawnAnimalFromRight", startDelay, spawnInterval * 3);
        InvokeRepeating("SpawnAnimalFromLeft", startDelay, spawnInterval * 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomAnimal()
    {
        // Randomly generate animals and spawn position 
        int animalIndex = Random.Range(0, animalPrefabs.Length); 
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ); 
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }
    
    void SpawnAnimalFromRight()
    {
        int zPosition = Random.Range(0, 6);
        int xPosition = 30;
        int yRotation = 270;

        // Randomly generate animals and spawn positions coming from right 
        int animalIndex = Random.Range(0, animalPrefabs.Length); 
        Vector3 spawnPos = new Vector3(xPosition, 0, zPosition); 
        Vector3 rotation = new Vector3(0, yRotation, 0);
        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(rotation));
    }

    void SpawnAnimalFromLeft()
    {
        int zPosition = Random.Range(0, 6);
        int xPosition = -30;
        int yRotation = 90;

        // Randomly generate animals and spawn positions coming from left
        int animalIndex = Random.Range(0, animalPrefabs.Length); 
        Vector3 spawnPos = new Vector3(xPosition, 0, zPosition); 
        Vector3 rotation = new Vector3(0, yRotation, 0);
        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(rotation));
    }


}
