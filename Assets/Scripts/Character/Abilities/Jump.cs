using System;
using UnityEngine;

public class Jump : AbilityBase
{
    [SerializeField] private float JumpForce = 100f;
    
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void OnUse()
    {
        rb.AddForce(Vector3.up*JumpForce);    
    }
}