using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   public static PlayerManager Instance { get; private set; }
   
   private PlayerController[] players;

   private void Awake()
   {
      Instance = this;
      players = FindObjectsOfType<PlayerController>();
   }

   public void AddPlayerToGame(Controller controller)
   {
      var firstNonActivePlayer = players.
         OrderBy(t => t.PlayerNumber).
         FirstOrDefault(t => !t.Initialized);
      if (firstNonActivePlayer != null) firstNonActivePlayer.InitializePlayer(controller);
   }

   public void SpawnPlayerCharacters()
   {
      foreach (var player in players)
      {
         if (player.Initialized && player.CharacterPrefab != null)
         {
            player.SpawnCharacter();
         }
      }
   }
}
