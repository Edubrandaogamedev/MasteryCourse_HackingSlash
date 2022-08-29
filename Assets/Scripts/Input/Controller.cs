using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private string attackButton;
    private string horizontalAxis;
    private string verticalAxis;
    
    public bool attack;
    public bool attackPressed;
    public float horizontal;
    public float vertical;
    public int Index { get; private set; }
    public bool IsAssigned { get; set; }
    
    private void Update()
    {
        if (!string.IsNullOrEmpty(attackButton))
        {
            attack = Input.GetButton(attackButton);
            attackPressed = Input.GetButtonDown(attackButton);
            horizontal = Input.GetAxis(horizontalAxis);
            vertical = Input.GetAxis(verticalAxis);
        }
    }
    private void DebugHelper(int controllerIndex)
    {
        gameObject.name = "Controller" + controllerIndex;
    }
    public void SetIndex(int index)
    {
        Index = index;
        attackButton = "Attack" + index;
        horizontalAxis = "Horizontal" + index;
        verticalAxis = "Vertical" + index;
        DebugHelper(index);
    }
    public bool AnyButtonDown()
    {
        return attack;
    }

    public Vector3 GetDirection()
    {
        return new Vector3(horizontal, 0, -vertical);
    }

}
