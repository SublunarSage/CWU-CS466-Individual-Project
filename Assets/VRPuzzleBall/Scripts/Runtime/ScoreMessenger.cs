using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMessenger : MonoBehaviour
{

    public int PointsToGrant = 0;
    public static event Action<MeshTriggerEvent> OnMeshTriggered;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnMeshTriggered?.Invoke(new MeshTriggerEvent(other.gameObject, PointsToGrant));
        }
    }
}