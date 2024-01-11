using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilMovement : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("TreasureCollider").GetComponent<TreasureMovement>().TreasureOpenEnvent += activeOil;
       
    }

    void Update()
    {
    }

    void activeOil()
    {
        gameObject.SetActive(true);

    }
}
