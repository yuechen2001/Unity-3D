using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    RoadSpawner roadSpawner; 
    // Start is called before the first frame update
    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Shifts road to next location once player drives past it 
    public void SpawnTriggerEntered()
    {
        roadSpawner.MoveRoad(); 
    }
}
