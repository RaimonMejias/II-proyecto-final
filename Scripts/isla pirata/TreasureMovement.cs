using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMovement : MonoBehaviour
{

    GameObject treasure;
    GameObject treasureTop;
    GameObject player;

    bool found = false;

    // Variables to control the movement
    Vector3 targetPosition;
    public float timeToTarget;
    float speed;
    float journeyLength;

    // Event to open the treasure
    public delegate void TreasureOpen();
    public event TreasureOpen TreasureOpenEnvent;

    // Start is called before the first frame update
    void Start()
    {
        // Get the treasure and the player
        treasure = GameObject.FindGameObjectWithTag("Treasure");
        treasureTop = GameObject.FindGameObjectWithTag("TreasureTop");
        player = GameObject.FindGameObjectWithTag("MainCamera");
        // Set the target position and calculate the speed
        targetPosition = new Vector3(69.09f,26.88f,-197.86f);
        treasure.SetActive(false);
        journeyLength = Vector3.Distance(treasure.transform.position, targetPosition);
        speed = journeyLength / timeToTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (found) {    // The traesure has been found
            if (Vector3.Distance(treasure.transform.position, targetPosition) < 0.001f) { // The treasure has reached the target position
                treasureTop.SetActive(false);
                TreasureOpenEnvent();
            } else { // The treasure has not reached the target position
                Vector3 position = player.transform.position;
                position.y = treasure.transform.position.y;
                treasure.transform.LookAt(position);
                treasure.transform.position = Vector3.MoveTowards(treasure.transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
        
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            treasure.SetActive(true);
            found = true;
        }
    }

}
