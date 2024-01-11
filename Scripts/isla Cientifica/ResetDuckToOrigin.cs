using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDuckToOrigin : MonoBehaviour {

    [Header("Internal Configuration")]
    private Vector3 startingPoint_;
    private Quaternion startingRotation_;

    void Start() {
        startingPoint_ = transform.position;
        startingRotation_ = transform.localRotation;    
    }

    private void OnTriggerEnter(Collider objectCollider) {
        GameObject collisionedObject = objectCollider.gameObject;
        if (collisionedObject.tag == "ResetBox") {
            GetComponent<Rigidbody>().Move(startingPoint_, startingRotation_);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


}
