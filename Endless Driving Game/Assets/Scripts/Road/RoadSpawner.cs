using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] roadPrefabs; 
    public float zSpawn = 0; 
    private float roadLength = 300; 
    public int numberOfRoads = 3; 
    public Transform playerTransform; 
    private List<GameObject> activeRoads = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Spawn 3 roads to start off
        for (int i = 0; i < ((byte)numberOfRoads); i++)
        {
            if (i == 0)
            {
                SpawnRoad(3);
            }
            SpawnRoad(Random.Range(0, roadPrefabs.Length - 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn new road ahead and delete road that player just passed 
        if (playerTransform.position.z - 300 > zSpawn - (numberOfRoads * roadLength))
        {
            SpawnRoad(Random.Range(0, roadPrefabs.Length - 1));
            DeleteTile();
        }
    }

    // Move previous road down to the end once the player passes spawntrigger
    public void SpawnRoad(int tileIndex)
    {
        GameObject newRoad = Instantiate(roadPrefabs[tileIndex]);
        newRoad.transform.position = new Vector3(2.490141f, -1.761234f, zSpawn);
        activeRoads.Add(newRoad);
        zSpawn += roadLength; 
    }

    private void DeleteTile()
    {
        Destroy(activeRoads[0]); 
        activeRoads.RemoveAt(0); 
    }
}
