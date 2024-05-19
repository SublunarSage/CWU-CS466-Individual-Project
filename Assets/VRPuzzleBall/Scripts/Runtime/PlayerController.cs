using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private GameObject spawnPoint;

    private Transform _marbleTransform;
    private Rigidbody _marbleRigidbody;
    private float _respawnThreshold = -10f;


    private void OnEnable()
    {
        ScoreManager.OnPlayerMessageSent += HandlePlayerMessage;
    }

    private void OnDisable()
    {
        ScoreManager.OnPlayerMessageSent -= HandlePlayerMessage;
    }

    private void Awake()
    {
        _marbleTransform = GetComponent<Transform>();
        _marbleRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_marbleTransform.position.y < spawnPoint.transform.position.y + _respawnThreshold)
        {
            Respawn();
        }
    }

    private void HandlePlayerMessage(PlayerMessage message, object data)
    {
        if (message == PlayerMessage.Respawn) Respawn();
        if (message == PlayerMessage.Deactivate) gameObject.SetActive(false);
    }

    private void Respawn()
    {
        // Get components again if they're null
        /*if (_marbleTransform == null || _marbleRigidbody == null)
        {
            _marbleTransform = GetComponent<Transform>();
            _marbleRigidbody = GetComponent<Rigidbody>();
        }*/
        
        if (spawnPoint.transform != null)
        {
            _marbleTransform.position = spawnPoint.transform.position;
            _marbleTransform.rotation = spawnPoint.transform.rotation;
        }
        else
        {
            _marbleTransform.position = Vector3.zero;
            _marbleTransform.rotation = new Quaternion(0,0,0,0);
        }
        
        _marbleRigidbody.velocity = Vector3.zero;
        _marbleRigidbody.angularVelocity = Vector3.zero;
    }
}
