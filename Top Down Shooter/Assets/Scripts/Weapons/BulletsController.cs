using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    public WeaponsInventory inventory;
    public float bulletSpeed;
    public float bulletRange; 

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
}
