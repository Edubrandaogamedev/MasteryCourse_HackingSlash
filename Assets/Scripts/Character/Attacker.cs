using System;
using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRate = 3f;
    [SerializeField] private float attackOffset = 1f;
    [SerializeField] private float radius = 1f;
    
    private float attackTimer;
    
    private Collider[] attackResults;
    public int Damage => damage;
    public bool CanAttack => attackTimer >= attackRate;

    private void Awake()
    {
        attackResults = new Collider[10];
        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        if (animationImpactWatcher != null)
            animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;
    }
    private void Update()
    {
        attackTimer += Time.deltaTime;
    }
    /// <summary>
    /// Called by animation event via AnimationImpactWatcher
    /// </summary>
    private void AnimationImpactWatcher_OnImpact()
    {
        
        var position = transform.position + transform.forward * attackOffset;
        int hitCount = Physics.OverlapSphereNonAlloc(position, radius, attackResults);
        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = attackResults[i].GetComponent<ITakeHit>();
            if (takeHit != null)
                takeHit.TakeHit(this);
        }
    }
    public void Attack(ITakeHit target)
    {
        attackTimer = 0;
        target.TakeHit(this);
    }
    
}
