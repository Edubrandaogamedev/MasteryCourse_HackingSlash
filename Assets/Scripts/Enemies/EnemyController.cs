using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ITakeHit
{
    [SerializeField] private GameObject impactParticle;
    
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    
    private Animator animator;
    private static readonly int DieAnim = Animator.StringToHash("Die");
    private static readonly int HitAnim = Animator.StringToHash("Hit");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeHit(Character hitBy)
    {
        currentHealth--;
        Instantiate(impactParticle, transform.position + (Vector3.up * 2), Quaternion.identity);
        if (currentHealth <= 0)
            Die();
        else
        {
            animator.SetTrigger(HitAnim);
        }
    }

    private void Die()
    {
        animator.SetTrigger((int) DieAnim);
        Destroy(gameObject, 3);
    }
}
