using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject boss; 
    private Rigidbody playerRb;
    private Rigidbody orbRb; 

    private float speed = 20.0f;
    private int damageToGive = 2;
    private float explosiveForce = 100.0f;
    private float orbLifespan = 5.0f;

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
        if (boss == null)
        {
            Destroy(gameObject); 
        }
        else
        {
            transform.Translate(boss.transform.forward * speed * Time.deltaTime);
        }

    }

    // When hit player, explode and deal damage 
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
            playerRb.AddForce(Vector3.forward * explosiveForce, ForceMode.Impulse);
            Destroy(gameObject);
        }

        // If orb hits the ground, destroy it
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        // Boss does friendly damage to enemies 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
            playerRb.AddForce(Vector3.forward * explosiveForce, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}