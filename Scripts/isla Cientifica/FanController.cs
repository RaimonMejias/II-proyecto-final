using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour {

    [Header("Fan Configuration")]
    public float fanDistance_ = 10.0f; 
    public float fanRadious_ = 5.0f;
    public float fanPower_ = 10.0f;

    [Header("Fan Target")]
    public string fanTarget_ = "Pato";

    [Header("Internal Configuration")]
    private GameObject hittedObject_ = null;
    private float currentHitDistance_ = 0.0f;
    private Vector3 radiousOffset_ = Vector3.zero;

    private void Start() {
        radiousOffset_ = new Vector3(0.0f, fanRadious_, 0.0f);
    }

    private void FixedUpdate() {
        RaycastHit hit = new RaycastHit();
        bool hittedSomething = Physics.SphereCast(
            transform.position - radiousOffset_,
            fanRadious_,
            transform.up,
            out hit,
            fanDistance_
        );
        if (hittedSomething && hit.collider.gameObject.tag == fanTarget_) {
            currentHitDistance_ = hit.distance;
            hittedObject_ = hit.collider.gameObject.transform.parent.gameObject;
            hittedObject_.GetComponent<Rigidbody>().AddForce(transform.up * fanPower_);
        } else {
            hittedObject_ = null;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = (hittedObject_)? Color.green : Color.red;
        Gizmos.DrawLine(
            transform.position, 
            transform.position + transform.up * currentHitDistance_
        );
        Gizmos.DrawWireSphere(
            transform.position + transform.up * currentHitDistance_, 
            fanRadious_
        );
    }
}
