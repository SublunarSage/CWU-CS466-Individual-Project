using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTriggerEvent
{
    public GameObject TriggeredBy { get; }
    public int Points { get;  }

    public MeshTriggerEvent(GameObject triggeredBy, int points)
    {
        TriggeredBy = triggeredBy;
        Points = points;
    }
}
