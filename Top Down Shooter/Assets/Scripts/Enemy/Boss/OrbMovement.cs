using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject boss; 
    private Rigidbody playerRb;
    private Rigidbody orbRb; 

    // Orb stats 
    private float speed = 20.0f;
    private float explosiveForce = 200.0f;
    private float orbLifespan = 5.0f;
    private int damageToGive = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss"); 
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        orbRb = GameObject.FindGameObjectWithTag("Boss Orb").GetComponent<Rigidbody>();

        // Destroy orb after a certain distance 
        Destroy(gameObject, orbLifespan); 
    }

    // Update is called once per frame
    void Update()
    {
        // Orb travels towards the direction the boss is facing 
        if (boss == null)
        {
            Destroy(gameObject); 
        }
        else
        {
            transform.Translate(boss.transform.forward * speed * Time.deltaTime);
        }
    }

    // When hit player or enemy, knockback and deal damage 
    public void OnCollisionEnter(Collision collision)
    {
        // If orb hits the ground, destroy it
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
            playerRb.AddForce(Vector3.forward * explosiveForce, ForceMode.Impulse);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
            playerRb.AddForce(Vector3.forward * explosiveForce, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}