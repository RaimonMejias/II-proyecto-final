using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveHat : MonoBehaviour
{
    GameObject player;
    bool pickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !pickedUp) {
            player = GameObject.FindGameObjectWithTag("MainCamera");
            transform.SetParent(player.transform);
            // Adjusts the local position and local rotation of the hat to appear on top of the player
            transform.localPosition = new Vector3(0, 1.1f, 0);
            transform.localRotation = Quaternion.identity; 

            // Plays the sound of the hat being picked up
            GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioReproductor>().StartTalking(1);
            pickedUp = true;

            GetComponent<movement_frame>().enabled = false;
        }
    }
}
