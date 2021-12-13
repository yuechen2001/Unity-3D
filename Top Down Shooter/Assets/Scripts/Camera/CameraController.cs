using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject focalPoint; 

    // Start is called before the first frame update
    void Start()
    {
        focalPoint = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        // Camera to give a top-down view and always follow player
        transform.position = focalPoint.transform.position + new Vector3(0, 30, -5); 
    }
}
