using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureController : MonoBehaviour {

    [Header("Internal Configuration")]
    private int currentPressure_ = 0;
    private bool isPressed_ = false;
    private float minScaleMesh_ = 0.03f;
    private GameObject objectAbove_;

    [Header("Expected Pressure")]
    public int expectedPressure_ = 0;

    private void AddPressureToMesh() {
        Vector3 meshScale = transform.localScale;
        float newScale = currentPressure_ * minScaleMesh_ / expectedPressure_; // Ver bien como hacer esto
        transform.localScale = new Vector3(meshScale.x, newScale, meshScale.z);
    }

    private void OnCollisionEnter(Collision objectCollision) {
        if (isPressed_) { return; }
        GameObject collisionedObject = objectCollision.collider.gameObject;
        if (collisionedObject.tag == "Pato") {
            isPressed_ = true;
            DuckBehaviour duckB = collisionedObject.transform.parent.GetComponent<DuckBehaviour>(); 
            currentPressure_ += (currentPressure_ < expectedPressure_)? duckB.duckPressure_ : 0;
            objectAbove_ = collisionedObject;
            AddPressureToMesh();
        } 
    }

    private void OnCollisionExit(Collision objectCollision) {
        GameObject collisionedObject = objectCollision.collider.gameObject;
        if (collisionedObject.tag == "Pato" && isPressed_) {
            if (collisionedObject != objectAbove_) { return; }
            DuckBehaviour duckB = collisionedObject.transform.parent.GetComponent<DuckBehaviour>();
            currentPressure_ -= (currentPressure_ > 0)? duckB.duckPressure_ : 0;
            AddPressureToMesh();
            isPressed_ = false;
            objectAbove_ = null;
        } 
        
    }
}
