using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController[] enemyPrefabs;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float respawnRate = 10;

    [SerializeField] private float initialSpawnDelay;
    
    [SerializeField] private int totalNumberToSpawn;
    
    [SerializeField] private int numberToSpawnEachTime = 1;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (ShouldSpawn())
            Spawn();
    }

    private bool ShouldSpawn()
    {
        return spawnTimer >= respawnRate;
    }

    private void Spawn()
    {
        spawnTimer = 0;
        var prefab = ChooseRandomEnemyPrefab();
        if (prefab != null)
        {
            var spawnPoint = ChooseRandomSpawnPoint();
            var enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
        
    }

    private Transform ChooseRandomSpawnPoint()
    {
        
        return spawnPoints.Length == 0 ? null : spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    private EnemyController ChooseRandomEnemyPrefab()
    {
        return enemyPrefabs.Length == 0 ? null : enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }
}
