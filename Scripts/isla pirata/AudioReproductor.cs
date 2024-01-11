using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReproductor : MonoBehaviour
{
    public List<AudioClip> talkingClips; // List of audio clips
    private AudioSource audioSource; // The audio source that will play the clips
    AudioSource MusicManager; // The audio source that will play the music

    // Variables to control the volume
    public float lowVolume = 0.01f;
    public float highVolume = 0.14f;
    public float fadeDuration = 0.5f; // How long it takes to fade the volume
    public bool isTalking = false;

    // Events to control the aparition and exit of the skull
    public delegate void SkullEntry();
    public event SkullEntry SkullEntryEvent;
    public delegate void SkullExit();
    public event SkullExit SkullExitEvent;
    
    void Start()
    {
        // Get the audio sources components
        audioSource = GetComponent<AudioSource>();
        MusicManager = GameObject.FindWithTag("MusicManager").GetComponent<AudioSource>();
    }

    // This function adjusts the volume of the music
    public void AdjustVolume(float targetVolume)
    {
        StartCoroutine(FadeVolume(fadeDuration, targetVolume));
    }

    // This function fades the volume of the music
    public IEnumerator FadeVolume(float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = MusicManager.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime; 
            MusicManager.volume = Mathf.Lerp(start, targetVolume, currentTime / duration); 
            yield return null;
        }
        yield break;
    }


    public void PreTalking()
    {
        SkullEntryEvent(); // Calls the event to make the skull appear
        AdjustVolume(lowVolume); // lowers the volume of the music
    }

    public bool StartTalking(int clipIndex)
    {
        // If the skull is not talking, it starts talking
        if (!isTalking)
        {
            isTalking = true;
            PreTalking();
            if(clipIndex >= 0 && clipIndex < talkingClips.Count && audioSource != null)
            {
                audioSource.clip = talkingClips[clipIndex]; // Set the clip to play
                audioSource.Play(); // Play the clip
                // waits until the clip is done playing to call the function that makes the skull disappear
                Invoke("DoneTalking", talkingClips[clipIndex].length);
            }
            return true;
        }
        return false;
    }

    // This function is called when the skull is done talking
    void DoneTalking()
    {
        AdjustVolume(highVolume); // Raises the volume of the music
        SkullExitEvent(); // Calls the event to make the skull disappear
        isTalking = false;
    }
}
