using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_frame : MonoBehaviour
{
    private Transform attacher;
    private GameObject player;
    public float hight = 2;
    private float yCenter;
    public float rotation = 0;
 
    void Start () {
        attacher = transform;
        yCenter = transform.position.y;
        player = GameObject.FindWithTag("Player");
    }
 
    void Update () {
        transform.position = new Vector3(transform.position.x, yCenter + Mathf.Sin(Time.time / 2) * hight / 2, transform.position.z);
        Vector3 position = player.transform.position;
        transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
        transform.Rotate(0, rotation, 0);
    }
}
