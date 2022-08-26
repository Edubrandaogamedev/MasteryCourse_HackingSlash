using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ControllerManager : MonoBehaviour
{
    private List<Controller> controllers;
    
    private void Awake()
    {
        controllers = FindObjectsOfType<Controller>().ToList();
        SetControllersIndex();
    }

    private void Update()
    {
        foreach (var controller in controllers)
        {
            if (!controller.IsAssigned && controller.AnyButtonDown())
                AssignController(controller);
        }
    }
    private void SetControllersIndex()
    {
        int index = 1;
        foreach (var controller in controllers)
        {
            controller.SetIndex(index);
            index++;
        }
    }

    private void AssignController(Controller controller)
    {
        controller.IsAssigned = true;
        FindObjectOfType<PlayerManager>().AddPlayerToGame(controller);
    }
    
}
