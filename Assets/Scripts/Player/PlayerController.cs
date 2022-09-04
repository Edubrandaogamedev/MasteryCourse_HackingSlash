using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    
    private UIPlayerText uiPlayerText;

    public event Action<Character> OnCharacterChanged = delegate {  };
    public bool Initialized => Controller != null;
    public int PlayerNumber => playerNumber;
    public Controller Controller { get; private set;}
    public Character CharacterPrefab { get; set; }

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

    public void SpawnCharacter()
    {
        var character = Instantiate(CharacterPrefab, new Vector3(10, 0, 12),Quaternion.identity);
        character.SetController(Controller);
        character.OnDied += OnPlayerDied;
        OnCharacterChanged(character);
    }

    private void OnPlayerDied(IDie character)
    {
        character.OnDied -= OnPlayerDied;
        Destroy(character.gameObject);
        StartCoroutine(RespawAfterDelay());
    }

    private IEnumerator RespawAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        SpawnCharacter();
    }

    private void DebugHelper(Controller controller)
    {
        gameObject.name = $"Player{playerNumber} - {controller.gameObject.name}";
    }
}
