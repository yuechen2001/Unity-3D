using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Rigidbody bossRB;
    private PlayerController player;
    public GameObject bossOrb;
    public Transform orbSpawnPoint;

    // Boss stats 
    private float moveSpeed = 2.5f;
    private float distanceToPlayer = 30f;
    private float shotTimer;
    private float shotInterval = 3.0f;

    private float playingAreaLimit = 170;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Boss looks in the direction of player and follows player to a certain distance
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        float range = Vector3.Distance(transform.position, player.transform.position);
        if (range > distanceToPlayer)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        // Fires orb at player at regular intervals 
        shotTimer += Time.deltaTime;
        if (shotTimer > shotInterval)
        {
            shotTimer = 0;
            FireOrb();
        }

        // Destroy boss if it is knocked out of playing area 
        if (transform.position.x > playingAreaLimit || transform.position.x < -playingAreaLimit || transform.position.z > playingAreaLimit || transform.position.z < -playingAreaLimit)
        {
            Destroy(gameObject);
        }
    }

    // Fire orbs which damages the player and knocks player back 
    private void FireOrb()
    {
        Instantiate(bossOrb, orbSpawnPoint.position, bossOrb.transform.rotation);
    }
}
