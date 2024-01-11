using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransform : MonoBehaviour
{

    [Header("Transform Config")]
    public Vector3 transformSpeed_ = new Vector3(0.0f, 0.0f, 0.0f);
    public float offsetLimit_ = 1.0f;
    private Vector3 originalPosition_;

    void Start() {
        originalPosition_ = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(originalPosition_, transform.position) >= offsetLimit_) {
            transformSpeed_ = -transformSpeed_;
        }
        transform.Translate(transformSpeed_);
    }
}
