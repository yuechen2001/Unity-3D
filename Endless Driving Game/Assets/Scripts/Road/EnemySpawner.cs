using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; 
    private Vector3 offset; 
    private Vector3 position = new Vector3(0, 0, 100);
    private float spawnTime = 3.3f; 
    private float timer = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > spawnTime)
        {
            int xPosition = Random.Range(0, 2) == 1 ? 15 : -15; 
            offset = new Vector3(xPosition, 0, 100);
            position = position + offset; 
            int rand = Random.Range(0, enemyPrefabs.Count);

            GameObject obs = Instantiate(enemyPrefabs[rand]);
            obs.transform.position = position;
            timer = 0;
            Destroy(obs, 60);
            position.x = 0;

        }

        timer += Time.deltaTime;
    }
}
