using System;
using Cinemachine;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    private CinemachineTargetGroup targetGroup;

    private void Awake()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
        var players = FindObjectsOfType<PlayerController>();
        foreach (var player in players)
        {
            player.OnCharacterChanged += (character) => OnCharacterChanged(player,character);
        }
    }

    private void OnCharacterChanged(PlayerController player, Character character)
    {
        int playerIndex = player.PlayerNumber - 1;
        targetGroup.m_Targets[playerIndex].target = character.transform;
    }
}
