using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField] private PlayerButton button;
    [SerializeField] private float attackRate = 3f;
    [SerializeField] protected string animationTrigger;
    
    private Controller controller;
    private Animator animator;
    
    protected float attackTimer;
    public bool CanAttack => attackTimer >= attackRate;
    
    protected abstract void OnUse();
    private void Update()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
        attackTimer += Time.deltaTime;
        if (ShouldTryUse())
        {
            if (!string.IsNullOrEmpty(animationTrigger))
                animator.SetTrigger(animationTrigger);
            OnUse();
        }
    }
    private bool ShouldTryUse()
    {
        return controller != null && CanAttack && controller.ButtonDown(button);
    }
    public void SetController(Controller controller)
    {
        this.controller = controller;
    }
}