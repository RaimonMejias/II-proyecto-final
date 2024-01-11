using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantenerEntreEscenas : MonoBehaviour {

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
