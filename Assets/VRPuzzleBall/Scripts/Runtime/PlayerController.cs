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

    private void Start()
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
    }

    private void Respawn()
    {
        _marbleTransform.position = spawnPoint.transform.position;
        _marbleTransform.rotation = spawnPoint.transform.rotation;
        _marbleRigidbody.velocity = Vector3.zero;
        _marbleRigidbody.angularVelocity = Vector3.zero;
    }
}
