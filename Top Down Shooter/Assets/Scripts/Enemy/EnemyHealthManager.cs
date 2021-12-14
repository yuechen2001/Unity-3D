using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int health;
    private int currentHealth; 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;    
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy enemy if enemy has no more health
        if (currentHealth <= 0)
        {
            Destroy(gameObject); 
        }
    }

    // Allows player to deal damage with weapons
    public void HurtEnemy(int damage)
    {
        currentHealth -= damage; 
    }
}
