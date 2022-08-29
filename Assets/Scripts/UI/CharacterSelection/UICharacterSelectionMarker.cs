using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectionMarker : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Image _markerImage;
    [SerializeField] private Image _lockImage;
    
    private UICharacterSelectionMenu menu;
    private bool _initializing;
    private bool _initialized;

    public bool IsLockedIn {get; private set;}
    public bool IsPlayerIn => _player.Initialized;

    private void Awake()
    {
        menu = GetComponentInParent<UICharacterSelectionMenu>();
        _markerImage.gameObject.SetActive(false);
        _lockImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!IsPlayerIn) return;
        if (!_initializing)
            StartCoroutine(Initialize());
        if (!_initialized) return;
        if (!IsLockedIn)
        {
            if (_player.Controller.horizontal > 0.5f)
            {
                MoveToCharacterPanel(menu.RightPanel);
            }
            else if (_player.Controller.horizontal < -0.5f)
            {
                MoveToCharacterPanel(menu.LeftPanel);
            }
            if (_player.Controller.attackPressed)
            {
                StartCoroutine(LockCharacter());
            }
        }
        else
        {
            if (_player.Controller.attackPressed)
            {
                menu.TryStartGame();
            }
        }
    }

    private IEnumerator LockCharacter()
    {
        _lockImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        IsLockedIn = true;
    }

    private void MoveToCharacterPanel(UICharacterSelectionPanel panel)
    {
        transform.position = panel.transform.position;
    }

    private IEnumerator Initialize()
    {
        _initializing = true;
        MoveToCharacterPanel(menu.LeftPanel);
        yield return new WaitForSeconds(0.5f);
        _markerImage.gameObject.SetActive(true);
        _initialized = true;
    }
}
