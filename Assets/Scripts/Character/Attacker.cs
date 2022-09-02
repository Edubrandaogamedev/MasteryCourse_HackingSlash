using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRate = 3f;
    
    private float attackTimer;
    public int Damage => damage;
    public bool CanAttack => attackTimer >= attackRate;
    
    public void Attack(ITakeHit target)
    {
        attackTimer = 0;
        target.TakeHit(this);
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }
}