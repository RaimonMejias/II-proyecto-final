using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{

  public Material celebrationCameraMaterial;
  public GameObject celebrationCamera;
  public GameObject oldCamera;

  void OnEnable() {
    lapCounter.OnRaceFinish += OnRaceFinish;
  }

  void OnDisable() {
    lapCounter.OnRaceFinish -= OnRaceFinish;
  }

  void OnRaceFinish() {
    celebrationCamera.SetActive(true);
    Debug.Log("CameraChange: OnRaceFinish");
    // change the actual material with celebrationCameraMaterial
    GetComponent<MeshRenderer>().material = celebrationCameraMaterial;
    oldCamera.SetActive(false);
  }
}
