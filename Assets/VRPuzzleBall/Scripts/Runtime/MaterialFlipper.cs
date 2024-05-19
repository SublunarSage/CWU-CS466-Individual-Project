using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFlipper : MonoBehaviour
{

    [SerializeField] private Material[] materials;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private int loopCount = 3;

    private Renderer objectRenderer;
    private int currentMaterialIndex = 0;
    private int currentLoopCount = 0;
    private bool isFlipping = false;
    
    private void OnEnable()
    {
        MeshTrigger.OnMeshTriggered += HandleMeshTriggered;
    }

    private void OnDisable()
    {
        MeshTrigger.OnMeshTriggered -= HandleMeshTriggered;
    }

    private IEnumerator FlipMaterials()
    {
        while (currentLoopCount < loopCount)
        {
            currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
            objectRenderer.material = materials[currentMaterialIndex];

            yield return new WaitForSeconds(delay);

            if (currentMaterialIndex == materials.Length - 1)
            {
                currentLoopCount++;
            }
        }
        // Reset back to first material
        objectRenderer.material = materials[0];

        isFlipping = false;
    }

    private void HandleMeshTriggered(MeshTriggerEvent<object> triggerEvent)
    {
        var eventData = triggerEvent.EventData;
        GameObject triggeredBy = (GameObject)eventData.GetType().GetProperty("TriggeredBy")?.GetValue(eventData);
        
        if (gameObject.transform.parent == triggeredBy.transform.parent && !isFlipping)
        {
            objectRenderer = GetComponent<Renderer>();
            isFlipping = true;
            StartCoroutine(FlipMaterials());
        }
    }
}
