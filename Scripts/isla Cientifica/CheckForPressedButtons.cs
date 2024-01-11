using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPressedButtons : MonoBehaviour {

    [Header("Buttons")]
    public List<DianaController> buttons_;
    public OpenDoor door_;

    void Update() {
        if (buttons_.Count == 0) { return; }
        foreach(DianaController button in buttons_) {
            if (!button.isPressed_) { return; }
        }
        door_.OpenDoorAndDestroy();
        Destroy(this);
    }
}
