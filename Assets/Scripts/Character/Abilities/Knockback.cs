using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : AbilityBase, IDamage
{
    [SerializeField] private float impactDelay = 0.25f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float radius = 2f;
    [SerializeField] private float forceAmount;

    private Collider[] attackResults;
    private LayerMask layerMask;
    public int Damage => damage;
    private void Awake()
    {
        attackResults = new Collider[10];
        string currentLayer = LayerMask.LayerToName(gameObject.layer);
        layerMask = ~LayerMask.GetMask(currentLayer);
    }
    protected override void OnUse()
    {
        Attack();
    }
    private void Attack()
    {
        StartCoroutine(DoAttack());
    }
    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(impactDelay);
        var position = transform.position + transform.forward;
        var hitCount = Physics.OverlapSphereNonAlloc(position, radius, attackResults,layerMask);
        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = attackResults[i].GetComponent<ITakeHit>();
            takeHit?.TakeHit(this);
            var hitRigidBody = attackResults[i].GetComponent<Rigidbody>();
            if (hitRigidBody == null) continue;
            var direction = hitRigidBody.transform.position - transform.position;
            direction.Normalize();
            hitRigidBody.AddForce(direction*forceAmount,ForceMode.Impulse);
        }
    }
}
