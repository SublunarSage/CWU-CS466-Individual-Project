using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeshMessage
{
    None,
    PlayerCollision
}

public class MeshTriggerEvent<T>
{
    public MeshMessage MeshMessage { get; }
    public T EventData { get;  }

    public MeshTriggerEvent(MeshMessage meshMessage, T eventData)
    {
        MeshMessage = meshMessage;
        EventData = eventData;
    }
}
