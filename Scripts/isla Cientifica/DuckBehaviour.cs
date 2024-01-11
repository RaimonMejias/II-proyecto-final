using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehaviour : MonoBehaviour {

    [Header("OnPointer Materials")]
    public Material inactiveMesh;
    public Material gazedAtMesh;

    [Header("Throwing Force")]
    public float force_ = 5.0f;

    [Header("Duck Internal Configuration")]
    private bool gazed_ = false;
    private bool isTaken_ = false;
    private bool isThrowable_ = false;
    private bool isdropable_ = false;
    private Rigidbody duckRB_;
    private AudioSource source_;

    [Header("Unused")]    
    public int duckPressure_ = 0;

    [Header("Camera Configuration")]
    private Transform cameraTf_;
    public float offsetFromCamera_ = 1.0f;

    void Start() {
        duckRB_ = GetComponent<Rigidbody>();
        source_ = GetComponent<AudioSource>();
        cameraTf_ = GameObject.FindWithTag("MainCamera").transform;
        SetMesh(false);
    }

    private void Update() {
        if (Input.GetButtonDown("Submit")) {
            if (isTaken_ && !isdropable_) { isThrowable_ = true; }
            else if (gazed_) { 
                isTaken_ = true; 
                OnPointerExit();
            }
            return;
        }
        if (Input.GetButtonDown("Cancel") && !isThrowable_) {
            isdropable_ = true;
        }
    }

    void FixedUpdate() {
        if (isTaken_) {
            FixTransformToPlayerCamera();
            if (isThrowable_) {
                ResetDuckProperties();
                ThrowDuck(); 
            } else if (isdropable_) { ResetDuckProperties(); }    
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.collider.gameObject.tag == "Player") { return; }
        if (source_ != null) { source_.Play(); }
        ScoreController.scoreValue_++;
    }

    private void OnTriggerEnter(Collider other) {
        ResetDuckProperties();
    }

    public void OnPointerEnter() {
        gazed_ = true;
        SetMesh(true);
    }

    public void OnPointerExit() {
        gazed_ = false;
        SetMesh(false);
    }

    public void OnPointerClick() {}

    private void FixTransformToPlayerCamera() {
        duckRB_.isKinematic = true;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        transform.parent = cameraTf_;
        transform.position = 
                cameraTf_.position + cameraTf_.forward * offsetFromCamera_;
    }

    private void ThrowDuck() {
        GetComponent<Rigidbody>().AddForce(cameraTf_.forward * force_);
    }

    private void SetMesh(bool gazedAt) {
        if (inactiveMesh != null && gazedAtMesh != null) {
            GetComponent<MeshRenderer>().material = 
                    (gazedAt)? gazedAtMesh : inactiveMesh;
        }
    }

    private void ResetDuckProperties() {
        isTaken_ = false;
        isThrowable_ = false;
        isdropable_ = false;
        duckRB_.isKinematic = false;
        transform.parent = null;
        gameObject.layer = LayerMask.NameToLayer("Interactiva");
    }

}
