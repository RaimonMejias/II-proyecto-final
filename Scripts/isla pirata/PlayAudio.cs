using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public int clipIndex = 0; // Index of the clip to reproduce
    GameObject AudioManager;
    bool reproduced = false;
    
    void Start()
    {
        // get the audio manager
        AudioManager = GameObject.FindWithTag("SoundManager");
    }

    void OnTriggerEnter(Collider other)
    {
        if (!reproduced) { // If the clip has not been reproduced yet
            reproduced = AudioManager.GetComponent<AudioReproductor>().StartTalking(clipIndex); // Play the clip
        }
    }
}
