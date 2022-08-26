using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    
    private Controller _controller;
    private UIPlayerText uiPlayerText;
    public bool Initialized => _controller != null;
    public int PlayerNumber => playerNumber;

    private void Awake()
    {
        uiPlayerText = GetComponentInChildren<UIPlayerText>();
    }

    public void InitializePlayer(Controller controller)
    {
        this._controller = controller;
        uiPlayerText.HandlePlayerInitialized();
        DebugHelper(controller);
    }

    private void DebugHelper(Controller controller)
    {
        gameObject.name = $"Player{playerNumber} - {controller.gameObject.name}";
    }
}
