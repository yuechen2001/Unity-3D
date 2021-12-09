using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool homing;

    private float rocketStrength = 15.0f;
    private float activeTimer = 5.0f; 

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
    }

    // Add force to GameObject when rocket hits it 
    void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            // If collided GameObject is the target, apply a force on target to push it away from player
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayDirection = -collision.contacts[0].normal;
                targetRb.AddForce(awayDirection * rocketStrength, ForceMode.Impulse);

                // Destroy rocket once collision is over 
                Destroy(gameObject); 
            }
        }
    }

    // Player fires rockets 
    public void Fire (Transform newTarget)
    {
        target = newTarget;
        homing = true; 

        // Destroy rocket if it has not hit the target after 5s
        Destroy(gameObject, activeTimer);
    }
}
