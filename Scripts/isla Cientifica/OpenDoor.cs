using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    [Header("Internal Configuration")]
    private Animator animator_;
    private float animLength_ = 1.0f;

    private void Start() {
        animator_ = GetComponent<Animator>();
    }

    public void OpenDoorAndDestroy() {
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation() {
        animator_.Play("Door Open");
        yield return new WaitForSeconds(animLength_);
        Destroy(gameObject);
    }
}
