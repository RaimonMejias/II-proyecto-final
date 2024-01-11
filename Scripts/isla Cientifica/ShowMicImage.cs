using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMicImage : MonoBehaviour {

    [Header("SpeechScript")]
    public SpeechRecognition speech_;

    private void Update() {
        GetComponent<Image>().enabled = speech_.isRecording_;
    }
}
