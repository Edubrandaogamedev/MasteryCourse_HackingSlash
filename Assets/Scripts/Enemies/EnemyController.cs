using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, ITakeHit , IAttack
{
    [SerializeField] private GameObject impactParticle;
    
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private Animator animator;
    private readonly int DieAnim = Animator.StringToHash("Die");
    private readonly int HitAnim = Animator.StringToHash("Hit");
    private readonly int SpeedAnim = Animator.StringToHash("Speed");
    private readonly int AttackAnim = Animator.StringToHash("Attack");

    private Character target;
    private NavMeshAgent navMeshAgent;
    private Attacker attacker;

    private bool IsDead => currentHealth <= 0;
    public int Damage => 1;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    private void Die()
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger((int) DieAnim);
        Destroy(gameObject, 3);
        
    }
    private void Update()
    {
        if (IsDead) return;
        if (target == null)
        {
            AdquireTarget();
        }
        else
        {
            var distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > 2)
            {
                FollowTarget();
            }
            else
            {
                TryAttack();
            }
        }
    }

    private void AdquireTarget()
    {
        target = Character.All
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
        animator.SetFloat(SpeedAnim, 0f);
    }

    private void FollowTarget()
    {
        navMeshAgent.isStopped = false;
        animator.SetFloat(SpeedAnim, 1f);
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void TryAttack()
    {
        animator.SetFloat(SpeedAnim, 0f);
        navMeshAgent.isStopped = true;
        if (!attacker.CanAttack) return;
        animator.SetTrigger(AttackAnim);
        attacker.Attack(target);
    }

    public void TakeHit(IAttack hitBy)
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
}