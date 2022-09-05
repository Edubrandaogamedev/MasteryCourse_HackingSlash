using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileAttacker : AbilityBase, IAttack
{
    
    [SerializeField] private int damage = 1;
    [SerializeField] private float launchYOffset = 1f;
    [SerializeField] private float specialAttackImpactDelay = 1f;
    [SerializeField] private Projectile projectilePrefab;

    private Animator animator;
    public int Damage => damage;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    protected override void OnUse()
    {
        Attack();
    }
    public void Attack()
    {
        StartCoroutine(LaunchAfterDelay());
    }

    private IEnumerator LaunchAfterDelay()
    {
        yield return new WaitForSeconds(specialAttackImpactDelay);
        projectilePrefab.Get<Projectile>(transform.position + Vector3.up*launchYOffset, transform.rotation);
    }
}
