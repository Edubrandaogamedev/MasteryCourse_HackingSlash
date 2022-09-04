using System;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour, ITakeHit, IDie
{
    public static List<Character> All = new List<Character>();
    
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int maxHealth = 10;
    
    private int currentHealth;
    
    private Controller controller;
    private AnimationImpactWatcher animationImpactWatcher;
    private Attacker attacker;
    
    private Animator animator;
    private static readonly int SpeedAnim = Animator.StringToHash("Speed");
    private static readonly int AttackAnim = Animator.StringToHash("Attack");
    
    
    public event Action<int,int> OnHealthChanged = delegate {  };
    public event Action<IDie> OnDied = delegate {  };
    public int Damage => damage;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        if (!All.Contains(this))
            All.Add(this);
    }

    private void OnDisable()
    {
        if (All.Contains(this))
            All.Remove(this);
    }

    private void Awake()
    {
        attacker = GetComponent<Attacker>();
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
            animator.SetFloat("Speed", direction.magnitude);
        }
        else
        {
            animator.SetFloat(SpeedAnim, 0);
        }

        if (controller.attackPressed)
        {
            if (attacker.CanAttack)
                animator.SetTrigger(AttackAnim);
        }
    }

    private void Die()
    {
        OnDied(this);
    }
    public void TakeHit(IAttack hitBy)
    {
        if (currentHealth <= 0) return;
        currentHealth -= hitBy.Damage;
        OnHealthChanged(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}