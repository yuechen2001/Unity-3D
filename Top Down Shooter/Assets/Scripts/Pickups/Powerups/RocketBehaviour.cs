using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Transform target;
    public ParticleSystem impactParticles;

    private float speed = 50.0f;
    private float rocketStrength = 20.0f;
    private bool homing;
    private int rocketDamage = 3; 

    // Start is called before the first frame update 
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // When player fires rocket, send the rocket towards the target 
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }

        // If target dies before collision, destroy rocket
        if (target == null)
        {
            Destroy(gameObject); 
        }
    }

    // Add force to GameObject when rocket hits it 
    void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                // If collided GameObject is the target, apply a force on target away from player
                Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayDirection = -collision.contacts[0].normal;
                targetRb.AddForce(awayDirection * rocketStrength, ForceMode.Impulse);

                // Get enemy's health manager and deal damage to enemy 
                EnemyHealthManager enemyHealthManager = collision.gameObject.GetComponent<EnemyHealthManager>(); 
                enemyHealthManager.HurtEnemy(rocketDamage);
                if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
                {
                    impactParticles.Play();
                }
                
                // Destroy rocket once collision is over 
                Destroy(gameObject, 0.1f);
            }
        }
    }

    // Player fires rockets 
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
    }
}
