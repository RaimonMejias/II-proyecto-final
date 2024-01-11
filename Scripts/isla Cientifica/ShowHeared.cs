using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowHeared : MonoBehaviour {

    [Header("SpeechScript")]
    public SpeechRecognition speech_;

    private void Update() {
        GetComponent<TMP_Text>().text = speech_.heared_;
    }
}
