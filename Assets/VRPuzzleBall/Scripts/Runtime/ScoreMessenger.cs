using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMessenger : MonoBehaviour
{

    public int PointsToGrant = 0;
    public static event Action<MeshTriggerEvent<object>> OnMeshTriggered;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            object eventData = new { Points = PointsToGrant };
            
            OnMeshTriggered?.Invoke(new MeshTriggerEvent<object>(MeshMessage.PlayerCollision, eventData));
        }
    }
}
