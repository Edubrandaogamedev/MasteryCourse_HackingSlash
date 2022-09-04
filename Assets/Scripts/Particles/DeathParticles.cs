using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    [SerializeField] private PooledMonoBehaviour deathParticlePrefab;
    
    private IDie entity;

    private void OnEnable()
    {
        entity = GetComponent<IDie>();
        entity.OnDied += OnEntityDied;
    }
    private void OnDisable()
    {
        entity.OnDied -= OnEntityDied;
    }

    private void OnEntityDied(IDie entity)
    {
        entity.OnDied -= OnEntityDied;
        deathParticlePrefab.Get<PooledMonoBehaviour>(transform.position,Quaternion.identity);
    }
}
