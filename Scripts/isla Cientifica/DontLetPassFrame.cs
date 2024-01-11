using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontLetPassFrame : MonoBehaviour {

    [Header("External Configuration")]
    public string target_ = "Pato";

    private void OnCollisionEnter(Collision collision) {
        GameObject gameObject = collision.collider.gameObject;
        if (gameObject.tag == target_) {
            Rigidbody duckRB_ = 
                gameObject.transform.parent.gameObject.GetComponent<Rigidbody>();
            duckRB_.velocity = new Vector3(0, 0, 0);
        }
    }

}
