using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    public GameObject[] pickupPrefabs;
    public GameObject[] powerupPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If enemy is killed, 25% chance to drop health pickups
    public void DropLoot()
    {
        int random = Random.Range(0, pickupPrefabs.Length);
        if (Random.Range(1f, 4f) == 1f)
        {
            Instantiate(pickupPrefabs[random], transform.position, pickupPrefabs[random].transform.rotation);
        }
    }

    // If boss is killed, 50% chance to drop powerups 
    public void DropPowerup()
    {
        int random = Random.Range(0, pickupPrefabs.Length);
        int dropChance = Random.Range(1, 3); 
        if (dropChance == 1)
        {
            Instantiate(powerupPrefabs[random], transform.position, powerupPrefabs[random].transform.rotation);
        }
    }
}
