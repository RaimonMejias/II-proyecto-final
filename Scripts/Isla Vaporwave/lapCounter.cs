using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lapCounter : MonoBehaviour
{
  public int lapCount = 3;


  public GameObject startLine;
  public GameObject finishLine;
  public GameObject[] checkpoints;
  public TMPro.TextMeshProUGUI lapText;
  public TMPro.TextMeshProUGUI celebrationText;

  private int currentLap = 1;
  private int currentCheckpoint = 0;
  private bool started = false;
  private bool finished = false;

  private float startTime;
  private float lapTime;
  private float bestLapTime = 99999999f;
  private float endTime;
  private float partialTime;

  public delegate void RaceDirector();
  public static event RaceDirector OnRaceStart;
  public static event RaceDirector OnRaceFinish;

  void Start() {
    lapText.text = "Lap " + currentLap + " / " + lapCount;
  }



  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("checkpoint")) {
      GameObject thisCheckpoint = other.gameObject;

      // Check if the race is not started
      if(thisCheckpoint == startLine && !started) {
        Debug.Log("Started");
        started = true;
        if(OnRaceStart != null) {
          OnRaceStart();
        }
        partialTime = lapTime = startTime = Time.time;
      } 
      // end the lap or race
      else if (thisCheckpoint == finishLine && started) {
        // if all laps are finished, finish the race
        if (currentLap == lapCount) {
          if (currentCheckpoint == checkpoints.Length) {
            Debug.Log("Finished");
            
            lapTime = Time.time - lapTime;
            if(lapTime < bestLapTime) {
              bestLapTime = lapTime;
            }
            endTime = Time.time;
            Debug.Log("Lap " + currentLap + " time: " + lapTime);
            Debug.Log("Total time: " + (endTime - startTime));
            Debug.Log("Best lap time: " + bestLapTime);
            if(!finished) {
              finished = true;
              lapText.text = "Finnish!" + "\n" + lapTime.ToString("F2") + "s" + "\n" + "Total time: " + (endTime - startTime) + "\n" + "Best lap time: " + bestLapTime;
              celebrationText.text = "Finnish!" + "\n" + lapTime.ToString("F2") + "s" + "\n" + "Total time: " + (endTime - startTime) + "\n" + "Best lap time: " + bestLapTime;

              if(OnRaceFinish != null) {
                Debug.Log("OnRaceFinish triggered");
                OnRaceFinish();
              }
            }
            
          } else {
            Debug.Log("Did not go through all checkpoints");
          } 
        } else if (currentLap < lapCount) {
          if(currentCheckpoint == checkpoints.Length) {
            currentLap++;
            currentCheckpoint = 0;
            lapTime = Time.time - lapTime;
            partialTime = Time.time - partialTime;
            Debug.Log("Checkpoint " + currentCheckpoint + " time: " + partialTime); 
            Debug.Log("Lap " + currentLap + " time: " + lapTime);
            lapText.text = "Lap " + currentLap + " / " + lapCount + "\n" + lapTime.ToString("F2") + "s";
            if(lapTime < bestLapTime) {
              bestLapTime = lapTime;
            }
          }
        } else {
          Debug.Log("Did not go through all checkpoints");
        }
      }

      // Check which checkpoint was passed
      for (int i = 0; i < checkpoints.Length; i++) {
        if (finished) return;

        if(thisCheckpoint == checkpoints[i]) {
          if (i == currentCheckpoint) {
            Debug.Log("Correct Checkpoint " + i + " passed");
            currentCheckpoint++;
            partialTime = Time.time - partialTime;
            Debug.Log("Checkpoint " + i + " time: " + partialTime);
          } else {
            Debug.Log("Incorrect Checkpoint " + i + " passed");
          }
        }
      }
    } 
  }
}
