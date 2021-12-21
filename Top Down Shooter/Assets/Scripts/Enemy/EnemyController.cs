using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyRB;
    private float moveSpeed = 5f;  
    private PlayerController player;

    // Start is called before the first frame update 
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy to follow player 
        transform.LookAt(player.transform.position);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); 
    }
}