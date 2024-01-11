using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullMovement : MonoBehaviour
{
    private GameObject player;
    // Variables to control smoothness of movement
    public float rotationSpeed = 5.0f;
    public float movementSpeed = 5.0f;
    public float distanceOfSkull = 3.0f;
    // Variable to control if the skull is in the scene
    public bool inScene = false;
    // Variable to control the animations of the skull
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        // Suscribe to the events of the audio manager
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioReproductor>().SkullEntryEvent += enter_skull;
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioReproductor>().SkullExitEvent += exit_skull;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (inScene) {
            // get the view player's direction and right
            Vector3 playerDirection = player.transform.forward;
            Vector3 rightOfPlayer = player.transform.right;

            // Rotates the object to face the player
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * Quaternion.Euler(0, 90, 0), rotationSpeed * Time.deltaTime);

            // Moves the object to be in front of the player
            Vector3 newPosition = player.transform.position + playerDirection * distanceOfSkull; 
            newPosition += rightOfPlayer * 1.5f; 
            transform.position = Vector3.MoveTowards(transform.position, newPosition, movementSpeed * Time.deltaTime);
        } 
        
    }

    // Function to enter the skull in the scene
    void enter_skull()
    {
        gameObject.SetActive(true);
        animator.Play("entry");
        // Place the skull in front of the player
        Vector3 playerDirection = player.transform.forward;
        transform.position = player.transform.position + playerDirection * distanceOfSkull;
        inScene = true;
    }

    // Function to exit the skull from the scene
    void exit_skull()
    {
        StartCoroutine(ActiveAfterAnimation(animator, "exit"));
        inScene = false;
    }

    // Function to wait until the animation ends
    IEnumerator ActiveAfterAnimation(Animator animator, string animationName)
    {
        // Play the animation
        animator.Play(animationName);
        // Wait until the animation ends
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animationLength);
        // Deactivate the object
        gameObject.SetActive(false);
    }
}
