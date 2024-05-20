using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonEvent : MonoBehaviour
{
    [SerializeField] private GameStateEvent gameStateEvent = GameStateEvent.None;
    [SerializeField] private string menuName;

    public static event EventHandler<UIButtonEventArgs> OnButtonEventTriggered;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(EmitEvent);
        }
    }

    private void OnDestroy()
    {
        if(button != null) button.onClick.RemoveListener(EmitEvent);
    }

    private void EmitEvent()
    {
        var eventArgs = new UIButtonEventArgs(gameStateEvent, menuName);
        OnButtonEventTriggered?.Invoke(this, eventArgs);
    }
}

public class UIButtonEventArgs : EventArgs
{
    public GameStateEvent GameStateEvent { get; }
    public string MenuName { get; }
    
    public UIButtonEventArgs(GameStateEvent gameStateEvent, string menuName)
    {
        GameStateEvent = gameStateEvent;
        MenuName = menuName;
    }
}