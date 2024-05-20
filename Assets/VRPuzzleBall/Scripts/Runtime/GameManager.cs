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
    Quit,
    OpenMenu,
    CloseMenu
}

public class GameManager : MonoBehaviour
{
    
    private void OnEnable()
    {
        UIButtonEvent.OnButtonEventTriggered += HandleButtonEvent;
    }

    private void OnDisable()
    {
        UIButtonEvent.OnButtonEventTriggered -= HandleButtonEvent;
    }
    

    private void HandleButtonEvent(object sender, UIButtonEventArgs args)
    {
        switch (args.GameStateEvent)
        {
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
