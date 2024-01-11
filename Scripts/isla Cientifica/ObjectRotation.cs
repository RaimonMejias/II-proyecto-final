using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour {

    [Header("Rotation Config")]
    public List<bool> inClockwiseDir_ = new List<bool>(3);
    public Vector3 speedVector_ = new Vector3(1.0f, 1.0f, 1.0f);
    public bool world_ = true;

    void Start() {
        for (int i = 0, counter = inClockwiseDir_.Count; i < counter; i++) {
            speedVector_[i] *= ((inClockwiseDir_[i])? 1 : -1);
        }
    }

    void Update() {
        transform.Rotate(speedVector_,  (world_)? Space.World: Space.Self);
    }
    
}
