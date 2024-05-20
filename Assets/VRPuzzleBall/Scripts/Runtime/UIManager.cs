using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static event Action<bool> OnToggleMuteAudio;
    
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Toggle muteAudioCheckbox;

    private void Awake()
    {
        muteAudioCheckbox.onValueChanged.AddListener((isMuted => OnToggleMuteAudio?.Invoke(!isMuted)));

        bool isMuted = PlayerPrefs.GetInt("MuteAudio", 0) == 1;
        AudioListener.volume = isMuted ? 0 : 1;
        muteAudioCheckbox.isOn = !isMuted;
        
        UIButtonEvent.OnButtonEventTriggered += HandleButtonEvent;
    }

    private void OnDestroy()
    {
        UIButtonEvent.OnButtonEventTriggered -= HandleButtonEvent;
        muteAudioCheckbox.onValueChanged.RemoveListener(isMuted => OnToggleMuteAudio?.Invoke(!isMuted));
    }

    private void HandleButtonEvent(object sender, UIButtonEventArgs args)
    {
        switch (args.GameStateEvent)
        {
            case GameStateEvent.None: break;
            case GameStateEvent.OpenMenu:
                OpenMenu(args.MenuName);
                break;
            case GameStateEvent.CloseMenu:
                CloseMenu(args.MenuName);
                break;
        }
    }
    

    public void OpenMenu(string menuName)
    {
        if (menuName == "SettingsPanel")
        {
            settingsPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void CloseMenu(string menuName)
    {
        if (menuName == "SettingsPanel")
        {
            settingsPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
