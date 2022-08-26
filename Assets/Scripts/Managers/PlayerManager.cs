using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   private PlayerController[] players;

   private void Awake()
   {
      players = FindObjectsOfType<PlayerController>();
   }

   public void AddPlayerToGame(Controller controller)
   {
      var firstNonActivePlayer = players.
         OrderBy(t => t.PlayerNumber).
         FirstOrDefault(t => !t.Initialized);
      if (firstNonActivePlayer != null) firstNonActivePlayer.InitializePlayer(controller);
   }
}
