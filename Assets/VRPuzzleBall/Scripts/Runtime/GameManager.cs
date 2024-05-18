using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRPuzzleBall.Scripts.Runtime;

public enum GameStateEvent
{
    None,
    Restart,
    Quit
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private ScoreManager _scoreManager;

    private void OnEnable()
    {
        PuzzleBoardController.OnGameStateEventSent += HandleGameStateMessage;
    }

    private void OnDisable()
    {
        PuzzleBoardController.OnGameStateEventSent -= HandleGameStateMessage;
    }

    private void HandleGameStateMessage(GameStateEvent message)
    {
        switch (message)
        {
            case GameStateEvent.None:
                break;
            case GameStateEvent.Restart:
                Restart();
                break;
            case GameStateEvent.Quit:
                QuitGame();
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
