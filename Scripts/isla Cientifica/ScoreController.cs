using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour {

    [Header("Internal Configuration")]
    private TMP_Text scoreText_;

    [Header("Score Value")]
    public static int scoreValue_;

    void Start() {
        scoreText_ = GetComponent<TMP_Text>();
        scoreValue_ = 0;
    }

    void Update() { scoreText_.text = $"{scoreValue_}";  }

}
