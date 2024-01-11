using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_controller : MonoBehaviour {
  public Rigidbody theBall;
  public float forwardAccel = 8f, reverseAccel = 50f, maxSpeed = 100f, turnStrength = 180f, deadZone = 0.05f, gravityForce = 10f, dragOnGround = 3f;
  public TMPro.TextMeshProUGUI debugText;

  private float speedInput, turnInput;

  private bool grounded;
  private bool isOnRoad;
  private float AxisAux;
  private Renderer rend;

  public LayerMask whatIsGround;
  public LayerMask whatIsRoad;
  public float groundRayLength = 0.5f;
  public Transform groundRayPoint;



  public Transform leftFrontWheel, rightFrontWheel;
  public float maxWheelTurn = 25f;

  public bool _debug = false;

  void Start() {
    theBall.transform.parent = null;
  }

  void Update() {

    speedInput = 0f;
    // use the inputs DriveF and DriveR to control the car
    if(!_debug) {
      if (Input.GetAxis("DriveF") > 0) {
        speedInput = Input.GetAxis("DriveF") * forwardAccel * 1000f;
      }
      if (Input.GetAxis("DriveR") > 0) {
        speedInput = Input.GetAxis("DriveR") * -reverseAccel * 1000f;
      }


      turnInput = Input.GetAxis("Horizontal");

      AxisAux = (speedInput > 0) ? Input.GetAxis("DriveF") : -Input.GetAxis("DriveR");
      if(grounded || isOnRoad) {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * AxisAux, 0f));
      }

      // Old rotation code
      // leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, 90);
      // rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, 90);

      transform.position = theBall.transform.position;
    } else {

      if (Input.GetAxis("Vertical") > 0) {
        speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
      }
      if (Input.GetAxis("Vertical") < 0) {
        speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
      }


      turnInput = Input.GetAxis("Horizontal");

      if(grounded || isOnRoad) {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
      }

      // Old rotation code
      // leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, 90);
      // rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, 90);

      transform.position = theBall.transform.position;

      debugText.text = "DriveF " + Input.GetAxis("DriveF");
      debugText.text += "\nDriveR " + Input.GetAxis("DriveR");
      debugText.text += "\nAxis3 " + Input.GetAxis("Axis3");
      debugText.text += "\nAxis4 " + Input.GetAxis("Axis4");
      debugText.text += "\nAxis5 " + Input.GetAxis("Axis5");
      debugText.text += "\nAxis6 " + Input.GetAxis("Axis6");
      debugText.text += "\nAxis7 " + Input.GetAxis("Axis7");
      debugText.text += "\nAxis8 " + Input.GetAxis("Axis8");
      debugText.text += "\nAxis9 " + Input.GetAxis("Axis9");
    }
  }

  void FixedUpdate() {
    grounded = false;
    isOnRoad = false;
    RaycastHit hit;
    if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround)) {
      // Debug.Log("grounded");
      grounded = true;

      transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
    } 
    if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsRoad)) {
      // Debug.Log("on road");
      isOnRoad = true;

      transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
    }

    if (grounded || isOnRoad) {
      theBall.drag = dragOnGround;
      if(Mathf.Abs(speedInput) > deadZone) {
        if (isOnRoad) {
          theBall.AddForce(transform.forward * speedInput);
        } else {
          theBall.AddForce(transform.forward * speedInput * 0.5f);
        }
      }
    } else {
      theBall.drag = 0.1f;
      theBall.AddForce(Vector3.up * -gravityForce * 100f);
    }
    // Debug.Log(theBall.velocity.magnitude);
  }
}
