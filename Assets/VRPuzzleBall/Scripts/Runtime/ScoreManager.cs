using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using Object = System.Object;

public class ScoreManager : MonoBehaviour
{
    public static event Action<PlayerMessage, object> OnPlayerMessageSent;

    [SerializeField] private int maxLives = 1;

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
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreTableText;
    private List<int> lifeScores = new List<int>();
    
    
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
        _totalScore = 0;
        lifeScores.Clear();
        UpdateLivesText();
        UpdateScoreText();
        OnPlayerMessageSent?.Invoke(PlayerMessage.Respawn,null);
    }
    

    private void UpdateScoreText()
    {
        scoreText.text = $"SCORE\n{TotalScore}";
    }

    private void UpdateLivesText()
    {
        livesText.text = $"LIVES: {CurrentLives}";
    }

    private void HandleMeshTriggered(MeshTriggerEvent<object> triggerEvent)
    {
        if (triggerEvent.MeshMessage == MeshMessage.PlayerCollision) HandlePlayerCollision(triggerEvent);
        
    }

    private void HandlePlayerCollision(MeshTriggerEvent<object> playerTriggerEvent)
    {
        var eventData = playerTriggerEvent.EventData;
        int points = (int)eventData.GetType().GetProperty("Points")?.GetValue(eventData);
        lifeScores.Add(points);
        TotalScore += points;
        
        if (CurrentLives > 0)
        {
            OnPlayerMessageSent?.Invoke(PlayerMessage.Respawn, null);
            
            CurrentLives--;
        }
        else
        {
            OnPlayerMessageSent?.Invoke(PlayerMessage.Deactivate,null);
            GameOver();
        }
    }
    
    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        UpdateScoreTableText();
    }

    private void UpdateScoreTableText()
    {
        string scoreTableString = "";
        for (int i = 0; i < lifeScores.Count; i++)
        {
            scoreTableString += "Life " + (i + 1) + ": " + lifeScores[i] + "\n";
        }

        scoreTableString += "Total Score: " + _totalScore;
        scoreTableText.text = scoreTableString;
    }
}
