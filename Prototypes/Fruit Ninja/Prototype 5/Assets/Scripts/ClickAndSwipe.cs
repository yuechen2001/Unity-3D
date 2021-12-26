using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera camera;
    private Vector3 mousePos;

    private TrailRenderer trail;
    private BoxCollider col;

    private bool swiping = false;

    private void Awake()
    {
        camera = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents(); 
            }
        }

        if (swiping)
        {
            UpdateMousePosition(); 
        }    
    }

    // Destroy game objects if hit 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget(); 
        }
    }

    // Set swiper object to move with mouse position 
    private void UpdateMousePosition()
    {
        mousePos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos; 
    }

    // Set components to boolean swiping 
    private void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping; 
    }
}
