using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    public WeaponsInventory inventory;

    public float bulletSpeed;
    public float bulletRange;
    public int bulletDamage = 1; 

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindWithTag("Gun").GetComponent<WeaponsInventory>(); 
        bulletSpeed = inventory.bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    // When a bullet collides with enemy, damage enemy. Destroy bullet after collision 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(bulletDamage);
            gameObject.SetActive(false); 
        }
    }
}
