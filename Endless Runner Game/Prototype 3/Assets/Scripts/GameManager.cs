using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score; 
    public float lerpSpeed; 
    public Transform startPoint; 
    private PlayerController playerControllerScript; 
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); 
        // Stop player input for starting animation 
        playerControllerScript.gameOver = true; 
        StartCoroutine(PlayIntro()); 
    }

    // Update is called once per frame
    void Update()
    {
        // Add score based on player's speed
        if (!playerControllerScript.gameOver)
        {
            if (playerControllerScript.doubleSpeed == true )
            {
                score += 2; 
            }
            else if (playerControllerScript.doubleSpeed == false )
            {
                score ++;
            }
            Debug.Log("Score: " + score);
        }
    }

    IEnumerator PlayIntro()
    {
         Vector3 startPos = playerControllerScript.transform.position;
         Vector3 endPos = startPoint.position; 
         float journeyLength = Vector3.Distance(startPos, endPos); 
         float startTime = Time.time; 

         float distanceCovered = (Time.time - startTime) * lerpSpeed; 
         float fractionOfJourney = distanceCovered / journeyLength; 

         playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f); 

         while (fractionOfJourney < 1)
         {
            distanceCovered = (Time.time - startTime) * lerpSpeed; 
            fractionOfJourney = distanceCovered / journeyLength; 
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney); 
            yield return null;
         }

        // After tweening character to start position, allow player input
         playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f); 
         playerControllerScript.gameOver = false;
    }
}
