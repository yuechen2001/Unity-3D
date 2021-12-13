using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera; 
    private Rigidbody playerRb;

    private float playerSpeed = 0.2f;
    private float playingFieldRange = 75;

    private GunController gun; 


    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.FindWithTag("Gun").GetComponent<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement 
        RestrictPlayingArea();
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * playerSpeed);
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * playerSpeed);

        // Using the mouse as an aiming point 
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength; 

        // Set player to look in the direction of the mouse
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
        }

        // Player can fire with left mouse-button. Releasing the button pauses the firing
        if (Input.GetMouseButtonDown(0))
        {
            gun.isFiring = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gun.isFiring = false; 
        }

    }

    // Prevent player from moving off the platform 
    private void RestrictPlayingArea()
    {
        if (transform.position.x > playingFieldRange)
        {
            transform.position = new Vector3(playingFieldRange, transform.position.y, transform.position.z); 
        }
        if (transform.position.x < -playingFieldRange)
        {
            transform.position = new Vector3(-playingFieldRange, transform.position.y, transform.position.z); 
        }
        if (transform.position.z > playingFieldRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, playingFieldRange);
        }
        if (transform.position.z < -playingFieldRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -playingFieldRange);
        }


    }
}
