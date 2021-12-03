using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim; 
    public ParticleSystem explosionParticle; 
    public ParticleSystem dirtParticle; 
    private AudioSource playerAudio;
    public AudioClip jumpSound; 
    public AudioClip crashSound; 
    public float jumpForce = 10; 
    public float doubleJumpForce; 
    private bool doubleJumpUsed; 
    public bool doubleSpeed;    
    public float gravityModifier;  
    private bool isOnGround = true; 
    public bool gameOver = false; 

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>(); 
        playerAudio = GetComponent<AudioSource>();
        // Adjust gravity in game 
        Physics.gravity *= gravityModifier; 

    }

    // Update is called once per frame
    void Update()
    {
        // Allows player to jump. Prevent player from more than double jumping or jumping when dead 
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
            isOnGround = false; 
            playerAnim.SetTrigger("Jump_trig"); 
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop(); 

            // Allow double jump 
            doubleJumpUsed = false; 
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver && !doubleJumpUsed)
        {
            playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse); 
            playerAnim.SetTrigger("Jump_trig"); 
            playerAudio.PlayOneShot(jumpSound, 1.0f); 
            doubleJumpUsed = true; 
        }

        // If player press L-Shift, activate doubleSpeed animation 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleSpeed = true; 
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        // Deactivate doubleSpeed when player releases L-Shift
        else if (doubleSpeed)
        {
            doubleSpeed = false; 
            playerAnim.SetFloat("Speed_Multiplier", 1.0f); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Allow player to jump again only after landing 
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; 
            dirtParticle.Play(); 
        }

        // If player collides with obstacle, game over 
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!"); 

            // Death animation
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1); 
            
            // Particle effects 
            dirtParticle.Stop();
            explosionParticle.Play(); 

            // Play sound effect 
            playerAudio.PlayOneShot(crashSound, 1.0f); 
        }
    }
}
