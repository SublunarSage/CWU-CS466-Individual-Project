using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBallLayer : MonoBehaviour
{

    public int LayerOnEnter; // BallInHole
    public int LayerOnExit; // BallOnBoard

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.layer = LayerOnEnter;
            Debug.Log("Bing");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.layer = LayerOnExit;
            Debug.Log("Bong");
        }
    }
}
