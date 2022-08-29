using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICharacterSelectionMenu : MonoBehaviour
{
   [SerializeField] private UICharacterSelectionPanel leftPanel,rightPanel;
   [SerializeField] private TextMeshProUGUI _startGameText;
   
   private UICharacterSelectionMarker[] markers;
   
   private bool startEnabled;
   public UICharacterSelectionPanel LeftPanel => leftPanel;
   public UICharacterSelectionPanel RightPanel => rightPanel;

   private void Awake()
   {
      markers = GetComponentsInChildren<UICharacterSelectionMarker>();
   }

   private void Update()
   {
      var playerCount = 0;
      var lockedCount = 0;
      foreach (var marker in markers)
      {
         if (marker.IsPlayerIn)
            playerCount++;
         if (marker.IsLockedIn)
            lockedCount++;
      }

      startEnabled = playerCount > 0 && playerCount == lockedCount;
      _startGameText.gameObject.SetActive(startEnabled);
   }

   public void TryStartGame()
   {
      if (startEnabled)
      {
         GameManager.Instance.Begin();
         gameObject.SetActive(false);
      }
   }
}
