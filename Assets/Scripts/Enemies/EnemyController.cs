using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : PooledMonoBehaviour, ITakeHit ,IDie
{
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
    public bool Alive { get; private set; }
    public int Damage => 1;
    
    public event Action<int, int> OnHealthChanged = delegate {  };
    public event Action<IDie> OnDied = delegate {  };
    public event Action OnHit = delegate {  };
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        attacker = GetComponent<Attacker>();
    }
    private void OnEnable()
    {
        currentHealth = maxHealth;
        Alive = true;
    }
    private void Update()
    {
        if (IsDead) return;
        if (target == null || !target.Alive)
        {
            AdquireTarget();
        }
        else
        {
            if (!attacker.InAttackRange(target))
            {
                FollowTarget();
            }
            else
            {
                TryAttack();
            }
        }
    }
    private void Die()
    {
        Alive = false;
        navMeshAgent.isStopped = true;
        animator.SetTrigger((int) DieAnim);
        OnDied(this);
        ReturnToPool(3);
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


    public void TakeHit(IDamage hitBy)
    {
        currentHealth-= hitBy.Damage;
        OnHealthChanged(currentHealth, maxHealth);
        OnHit();
        if (currentHealth <= 0)
            Die();
        else
        {
            animator.SetTrigger(HitAnim);
        }
    }

}