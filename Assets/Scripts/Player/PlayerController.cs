using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    
    private UIPlayerText uiPlayerText;
    public bool Initialized => Controller != null;
    public int PlayerNumber => playerNumber;
    public Controller Controller { get; private set;}

    private void Awake()
    {
        uiPlayerText = GetComponentInChildren<UIPlayerText>();
    }

    public void InitializePlayer(Controller controller)
    {
        Controller = controller;
        uiPlayerText.HandlePlayerInitialized();
        DebugHelper(controller);
    }

    private void DebugHelper(Controller controller)
    {
        gameObject.name = $"Player{playerNumber} - {controller.gameObject.name}";
    }
}
