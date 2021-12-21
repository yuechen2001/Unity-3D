using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealthManager : MonoBehaviour
{
    private EnemyLootDrop enemyLootDrop;

    public Slider enemyHealthBar;
    private int currentHealth; 
    public int maxHealth;
    private int damageTaken;


    // Start is called before the first frame update
    void Start()
    {
        enemyLootDrop = GetComponent<EnemyLootDrop>(); 
        currentHealth = maxHealth; 

        // Slider to show enemy health 
        enemyHealthBar.maxValue = maxHealth;
        damageTaken = 0;
        enemyHealthBar.fillRect.gameObject.SetActive(false);
    }

    // Allow player to deal damage with weapons. Destroy enemy if no more health 
    public void HurtEnemy(int damage)
    {
        currentHealth -= damage; 
        HurtEnemyHealthBar(damage);

        if (damageTaken >= maxHealth)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                // Enemy has a chance of dropping loot when killed
                enemyLootDrop.DropLoot();
            }
            else if (gameObject.CompareTag("Boss"))
            {
                enemyLootDrop.DropLoot(); 
            }

            Destroy(gameObject); 
        }
    }

    // Reflect damage taken by enemy on healthbar
    public void HurtEnemyHealthBar(int amount)
    {
        damageTaken += amount;
        enemyHealthBar.fillRect.gameObject.SetActive(true);
        enemyHealthBar.value = damageTaken;
    }

}
