using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Controller controller;
    
    private Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    [SerializeField] private float movementSpeed = 5f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>(); 
    }

    public void SetController(Controller controller)
    {
        this.controller = controller;
    }

    private void Update()
    {
        
        var direction = controller.GetDirection();
        if (direction.magnitude > 0.2f)
        {
            transform.position += direction * Time.deltaTime * movementSpeed;
            transform.forward = direction * 360f;
            animator.SetFloat("Speed",direction.magnitude);
        }
        else
        {
            animator.SetFloat(Speed,0);
        }
    }
}
