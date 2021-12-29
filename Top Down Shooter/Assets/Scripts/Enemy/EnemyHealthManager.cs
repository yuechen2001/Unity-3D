using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    private EnemyLootDrop enemyLootDrop;
    public ParticleSystem bloodSplurt; 

    // Enemy healthbar stats 
    public Slider enemyHealthBar;
    private int damageTaken;
    public int currentHealth;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyLootDrop = GetComponent<EnemyLootDrop>();
        currentHealth = maxHealth;

        // Slider as enemy healthbar 
        enemyHealthBar.maxValue = maxHealth;
        damageTaken = 0;
        enemyHealthBar.fillRect.gameObject.SetActive(false);
    }

    // Allow player to deal damage with weapons. Destroy enemy if no more health 
    public void HurtEnemy(int damage)
    {
        currentHealth -= damage; 
        HurtEnemyHealthBar(damage);
        bloodSplurt.Play(); 

        if (damageTaken >= maxHealth)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                // Enemy has a chance of dropping loot when killed
                enemyLootDrop.DropLoot();
            }
            else if (gameObject.CompareTag("Boss"))
            {
                // Boss drops different powerups when killed 
                enemyLootDrop.DropPowerup(); 
            }
            Destroy(gameObject); 
        }
    }

    // Reflect damage taken by enemy on healthbar
    public void HurtEnemyHealthBar(int amount)
    {
        damageTaken += amount;
        if (damageTaken > 0 && currentHealth < maxHealth)
        {
            enemyHealthBar.fillRect.gameObject.SetActive(true);
            enemyHealthBar.value = damageTaken;
        }
    }
}
