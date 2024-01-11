using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceMinigameController : MonoBehaviour {

    [Header("VoiceMinigame")]
    public VoiceGame minigame_;

    [Header("Dependencies")]
    public ShowMicImage showMic_;
    public ShowHeared showHeared_;

    [Header("Door")]
    public OpenDoor door_;
    
    void Update() {
        if (minigame_.isWon_) { 
            door_.OpenDoorAndDestroy();
            Destroy(this);
            return;
        }
        if (minigame_.isGameover_) { minigame_.Reset(); }    
    }
}
