using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    public GameObject[] pickupPrefabs;
    public GameObject[] powerupPrefabs;

    private float lootYOffset = 2.0f; 

    // If enemy is killed, 25% chance to drop health pickups
    public void DropLoot()
    {
        int random = Random.Range(0, pickupPrefabs.Length);
        int dropChance = Random.Range(1, 5);
        if (dropChance == 1)
        {
            Instantiate(pickupPrefabs[random], transform.position, pickupPrefabs[random].transform.rotation);
        }
    }

    // If boss is killed, drops one of the powerups 
    public void DropPowerup()
    {
        int random = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[random], new Vector3(transform.position.x, lootYOffset, transform.position.z), powerupPrefabs[random].transform.rotation);
    }
}
