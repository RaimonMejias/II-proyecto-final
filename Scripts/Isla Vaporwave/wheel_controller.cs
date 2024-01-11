using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheel_controller : MonoBehaviour
{

  public GameObject[] wheelsToRotate;
  public float rotationSpeed = 100f;

  // Start is called before the first frame update
  void Start() {
    
  }

  // Update is called once per frame
  void Update() {
    float verticalAxis = -Input.GetAxis("Vertical");

    foreach (GameObject wheel in wheelsToRotate) {
      wheel.transform.Rotate(0, Time.deltaTime * verticalAxis * rotationSpeed, 0, Space.Self);
    }
  }
}
