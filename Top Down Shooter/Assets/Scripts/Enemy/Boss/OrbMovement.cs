using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbMovement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRb; 
    private float speed = 10.0f;
    private int damageToGive = 2;
    private float explosiveForce = 100.0f; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime); 
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
    }

}
