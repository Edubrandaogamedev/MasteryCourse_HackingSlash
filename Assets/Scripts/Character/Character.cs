using System;
using System.Collections.Generic;
using UnityEngine;
public class Character : PooledMonoBehaviour, ITakeHit, IDie
{
    public static List<Character> All = new List<Character>();
    
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int maxHealth = 10;
    
    private int currentHealth;
    
    private Controller controller;
    private AnimationImpactWatcher animationImpactWatcher;
    private IAttack attacker;
    private Rigidbody rb;
    
    private Animator animator;
    private static readonly int SpeedAnim = Animator.StringToHash("Speed");
    
    public event Action<int,int> OnHealthChanged = delegate {  };
    public event Action<IDie> OnDied = delegate {  };
    public event Action OnHit = delegate {  };
    public int Damage => damage;
    public bool Alive { get; private set; }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        Alive = true;
        if (!All.Contains(this))
            All.Add(this);
    }

    protected override void OnDisable()
    {
        if (All.Contains(this))
            All.Remove(this);
        base.OnDisable();
    }

    private void Awake()
    {
        attacker = GetComponent<IAttack>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    public void SetController(Controller controller)
    {
        this.controller = controller;
        foreach (var ability in GetComponents<AbilityBase>())
        {
            ability.SetController(controller);    
        }
    }

    private void Update()
    {

        var direction = controller.GetDirection();
        if (direction.magnitude > 0.2f)
        {
            var velocity = (direction * movementSpeed).With(y:rb.velocity.y);
            rb.velocity = velocity;
            transform.forward = direction * 360f;
            animator.SetFloat("Speed", direction.magnitude);
        }
        else
        {
            animator.SetFloat(SpeedAnim, 0);
        }
    }

    private void Die()
    {
        Alive = false;
        OnDied(this);
    }
    public void TakeHit(IDamage hitBy)
    {
        if (currentHealth <= 0) return;
        currentHealth -= hitBy.Damage;
        OnHit();
        OnHealthChanged(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

}