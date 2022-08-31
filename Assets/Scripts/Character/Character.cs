using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Controller controller;
    
    private Animator animator;
    private static readonly int SpeedAnim = Animator.StringToHash("Speed");
    private static readonly int AttackAnim = Animator.StringToHash("Attack");
    
    [SerializeField] private float movementSpeed = 5f;
    
    [SerializeField] private float attackOffset = 1f;
    [SerializeField] private float radius = 1f;
    
    private Collider[] attackResults;
    private AnimationImpactWatcher animationImpactWatcher;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attackResults = new Collider[10];

        animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;
    }

    private void OnDestroy()
    {
        animationImpactWatcher.OnImpact -= AnimationImpactWatcher_OnImpact;
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
            animator.SetTrigger(AttackAnim);
        }
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
}
