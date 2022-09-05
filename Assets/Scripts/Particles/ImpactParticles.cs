using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticles : PooledMonoBehaviour
{
    [SerializeField] private PooledMonoBehaviour impactParticle;

    private ITakeHit entity;
    private void Awake()
    {
        entity = GetComponent<ITakeHit>();
        entity.OnHit += HandleHit;
    }
    private void OnDestroy()
    {
        entity.OnHit -= HandleHit;
    }
    private void HandleHit()
    {
        impactParticle.Get<PooledMonoBehaviour>(transform.position + (Vector3.up * 2), Quaternion.identity);
    }
}
