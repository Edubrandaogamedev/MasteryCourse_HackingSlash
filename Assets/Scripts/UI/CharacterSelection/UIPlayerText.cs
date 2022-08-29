using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerText : MonoBehaviour
{
    private TextMeshProUGUI _tmText;

    private void Awake()
    {
        _tmText = GetComponent<TextMeshProUGUI>();
    }

    public void HandlePlayerInitialized()
    {
        _tmText.text = "Player Joined";
        StartCoroutine(ClearTextAfterDelay());
    }

    private IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(2);
        _tmText.text = string.Empty;
    }
}
