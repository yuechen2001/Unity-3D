using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerDashboard : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0.02f, 4.5f, 1.2f); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Offset camera behind the player by adding to player position 
        transform.position = player.transform.position + offset;
    }
}
