using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private string attackButton;
    public bool attack;
    public int Index { get; private set; }
    public bool IsAssigned { get; set; }
    
    private void Update()
    {
        if (!string.IsNullOrEmpty(attackButton))
            attack = Input.GetButton(attackButton);
    }
    public void SetIndex(int index)
    {
        Index = index;
        attackButton = "Attack" + index;
        DebugHelper(index);
    }
    public bool AnyButtonDown()
    {
        return attack;
    }

    private void DebugHelper(int controllerIndex)
    {
        gameObject.name = "Controller" + controllerIndex;
    }
}
