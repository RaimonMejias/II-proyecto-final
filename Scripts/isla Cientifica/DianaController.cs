using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaController : MonoBehaviour {

    [Header("External Configuration")]
    public bool isVertical_ = false;
    public bool isPressed_ = false; 
    public Material pressedMaterial_;
    public Material unpressedMaterial_;
    
    [Header("Internal Configuration")]
    private Vector3 startingPos_;
    private Vector3 hexagonPos_;

    private void Start() {
        startingPos_ = transform.position;
        hexagonPos_ = transform.parent.GetChild(5).position;
    }

    private void OnCollisionEnter(Collision objectCollision) {
        GameObject collisionedObject = objectCollision.collider.gameObject;
        if (!isPressed_ && collisionedObject.tag == "Pato") {
            MoveBackward();
        } 
    }

    private void MoveBackward() {
        if (isVertical_) {
            transform.position = 
                new Vector3(hexagonPos_.x, startingPos_.y, startingPos_.z);
        } else {
            transform.position = 
                new Vector3(startingPos_.x, hexagonPos_.y, startingPos_.z);
        }
        GetComponent<MeshRenderer>().material = pressedMaterial_;
        isPressed_ = true;
    }

    private void MoveForward() {
        transform.position = startingPos_;
        GetComponent<MeshRenderer>().material = unpressedMaterial_;
        isPressed_ = false;
    }

}
