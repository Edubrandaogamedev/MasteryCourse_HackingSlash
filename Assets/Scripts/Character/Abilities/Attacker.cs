using System;
using System.Collections;
using UnityEngine;

public class Attacker : AbilityBase, IAttack
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRange = 2f;
    
    [SerializeField] private float attackOffset = 1f;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float attackImpactDelay = 1f;
    
    private LayerMask layerMask;
    
    private Collider[] attackResults;
    
    private Animator animator;
    public int Damage => damage;
    
    private void Awake()
    {
        string currentLayer = LayerMask.LayerToName(gameObject.layer);
        layerMask = ~LayerMask.GetMask(currentLayer);
        animator = GetComponentInChildren<Animator>();
        attackResults = new Collider[10];
        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        if (animationImpactWatcher != null)
            animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;
    }
    
    /// <summary>
    /// Called by animation event via AnimationImpactWatcher
    /// </summary>
    private void AnimationImpactWatcher_OnImpact()
    {
        
        var position = transform.position + transform.forward * attackOffset;
        var hitCount = Physics.OverlapSphereNonAlloc(position, radius, attackResults,layerMask);
        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = attackResults[i].GetComponent<ITakeHit>();
            takeHit?.TakeHit(this);
        }
    }
    private IEnumerator DoAttack(ITakeHit target)
    {
        yield return new WaitForSeconds(attackImpactDelay);
        if (target.Alive && InAttackRange(target))
        {
            target.TakeHit(this);
        }
    }
    public void Attack()
    {
        animator.SetTrigger(animationTrigger);
    }
    public void Attack(ITakeHit target)
    {
        attackTimer = 0;
        StartCoroutine(DoAttack(target));
    }
    public bool InAttackRange(ITakeHit target)
    {
        if (!target.Alive) return false;
        var distance = Vector3.Distance(transform.position, target.transform.position);
        return distance < attackRange;
    }

    protected override void OnUse()
    {
        Attack(); 
    }
}