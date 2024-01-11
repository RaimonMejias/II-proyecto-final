using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OctopusMovement : MonoBehaviour
{

    GameObject player;

    // Variables to control the movement
    bool attack = false;
    bool inFace = false;
    bool dead = false;
    bool canKill = false;
    public float speedThreshold = 1.5f;
    public Text text;
    // Variable to control the next scene
    public GameObject nextScene;
    public bool _debug = false;

    void Start()
    {
        nextScene.SetActive(false);
        player = GameObject.FindGameObjectWithTag("MainCamera");

    }


    void Update()
    {
        Vector3 position = player.transform.position;
        // Check if the octopus is not already on the face and is not dead
        if (!inFace && !dead)
        {
            // calculate de distance with the player to see if it should attack or attach to the face
            CalculateDistance();
            // If the octopus is set to attack
            if (attack)
            {
                // Move octopus towards the player's position
                position.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, position, 0.1f);
            }
            LookAtPlayer(position);
        }
        else if (!dead) // If the octopus is in the face
        {

            // Attach the octopus slightly in front of the player
            Vector3 playerDirection = player.transform.forward;
            Vector3 newPosition = player.transform.position + (playerDirection * 10)/8;
            newPosition.y = position.y - 0.5f;
            transform.position = newPosition;
            Vector3 aceleration = Input.acceleration;
            if ((aceleration.magnitude >= speedThreshold && canKill) || (canKill && Input.GetKey(KeyCode.Space)))
            {
                dead = true;
                inFace = false;
                
                // start talking and wait for the clip to end
                StartCoroutine(StartTalkingAndWait(3));
            }   
            LookAtPlayer(position);
        } else { // If the octopus is dead
            Vector3 targetPosition = new Vector3(87.52f,37.20f,-25.89f);
            Quaternion targetRotation = Quaternion.Euler(0, 0, 180);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 20f * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100f* Time.deltaTime);
        }
        
        
    }

    // Function to make the octopus look at the player
    private void LookAtPlayer(Vector3 position)
    {
        Vector3 lookPosition = new Vector3(position.x, transform.position.y, position.z);
        transform.LookAt(lookPosition);
        transform.Rotate(0, 270, 0); 
    }

    // Function to calculate the distance between the player and the octopus
    void CalculateDistance()
    {
        Vector3 position = player.transform.position;
        float distance = Vector3.Distance(position, transform.position);
        if (distance >= 3 &&  distance < 10) // Ready to attack
        {
            attack = true;
        } else if (distance < 3) // Ready to attach to the face
        {
           attack = false;
           inFace = true;
           StartCoroutine(StartTalkingAndWait(2));
        }
       
    }

    IEnumerator StartTalkingAndWait(int clipIndex)
    {
        // Find the component and call the StartTalking method.
        AudioReproductor audioReproductor = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioReproductor>();
        audioReproductor.StartTalking(clipIndex);
        // Wait until the audio clip finishes playing.
        float clipLength = audioReproductor.talkingClips[clipIndex].length;
        yield return new WaitForSecondsRealtime(clipLength+2.3f);
        if (clipIndex == 2) // Audio clip of getting in trouble about the octopus
        {
            canKill = true;
        } else if (clipIndex == 3) // Audio clip of the octopus dying
        {
            nextScene.SetActive(true);
        }
    }
}
