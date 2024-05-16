using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ScoreManager : MonoBehaviour
{
    public static event Action<PlayerMessage, object> OnPlayerMessageSent;

    [SerializeField] private int maxLives = 5;

    private int _currentLives;
    private int _totalScore;

    private int CurrentLives
    {
        get => _currentLives;
        set
        {
            _currentLives = value;
            UpdateLivesText();
        }
    }

    private int TotalScore
    {
        get => _totalScore;
        set
        {
            _totalScore = value;
            UpdateScoreText();
        }
    }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    
    private void OnEnable()
    {
        ScoreMessenger.OnMeshTriggered += HandleMeshTriggered;
    }

    private void OnDisable()
    {
        ScoreMessenger.OnMeshTriggered -= HandleMeshTriggered;
    }

    private void Start()
    {
        CurrentLives = maxLives;
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"SCORE\n{TotalScore}";
    }

    private void UpdateLivesText()
    {
        livesText.text = $"LIVES: {CurrentLives}";
    }

    private void HandleMeshTriggered(MeshTriggerEvent triggerEvent)
    {
        //Debug.Log("Mesh triggered by: " + triggerEvent.TriggeredBy.name);
        //Debug.Log("Points to grant: " + triggerEvent.Points);
        TotalScore += triggerEvent.Points;

        if (CurrentLives > 0)
        {
            OnPlayerMessageSent?.Invoke(PlayerMessage.Respawn, null);
            CurrentLives--;
        }
        
    }
}
