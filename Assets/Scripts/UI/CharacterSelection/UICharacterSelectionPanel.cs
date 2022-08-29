using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelectionPanel : MonoBehaviour
{
    [SerializeField] private Character _characterPrefab;
    
    public Character CharacterPrefab => _characterPrefab;

    
}
