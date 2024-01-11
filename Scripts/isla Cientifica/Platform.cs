using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    [Header("Platform Configuration")]
    public Platform[] neighbors_;
    public bool isButton_ = false;
    public bool isPressed_ = false;
    private int neighborsSize_ = 4;

    [Header("Object Above")]
    public GameObject objectAbove_;
    private string target_ = "PatoGris";

    [Header("Button Materials")]
    public Material pressedMaterial_;
    public Material unpressedMaterial_;

    private void Start() {
        objectAbove_ = null;
        neighbors_ = new Platform[neighborsSize_];
    }

    private void Update() {
        if (isButton_) {
            isPressed_ = objectAbove_ && objectAbove_.name == target_;
            transform.GetChild(4).gameObject.GetComponent<MeshRenderer>().material = 
                (isPressed_)? pressedMaterial_: unpressedMaterial_;
        }
    }

}
