using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckVoiceResponse : MonoBehaviour {

    [Header("Event Configuration")]
    public bool goInverse_ = false;

    [Header("Internal Configuration")]
    private int maxDirectionValue_ = 3;
    private SpeechRecognition recognition_;
    private GameObject currentPlatform_;
    
    void Start() {
        recognition_ = transform.parent.parent.gameObject.GetComponent<SpeechRecognition>();
        recognition_.MoveDuck += DuckMovement;
        currentPlatform_ = transform.parent.gameObject;
    }

    private void DuckMovement(int direction) {
        int index = (goInverse_)? (int)Mathf.Abs(direction - maxDirectionValue_) : direction;
        try {
            GameObject nextPlatform = currentPlatform_.GetComponent<Platform>().neighbors_[index].gameObject;
            if (nextPlatform.GetComponent<Platform>().objectAbove_) {
                throw new ApplicationException();
            }
            SetCurentPlatform(nextPlatform);
        } catch(Exception e) {
            if (e.GetType().FullName == "System.ApplicationException") {
                transform.parent.parent.gameObject.GetComponent<VoiceGame>().GameOver(true);
                return;
            }
        }
    }

    public void SetCurentPlatform(GameObject newCurrentPlatform) {
        currentPlatform_.GetComponent<Platform>().objectAbove_ = null;
        transform.parent = newCurrentPlatform.transform;
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
        currentPlatform_ = newCurrentPlatform;
        currentPlatform_.GetComponent<Platform>().objectAbove_ = gameObject;
    }
}
