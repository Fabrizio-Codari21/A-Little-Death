using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "ScriptableObjects/EnemyManager")]
public class EnemyManager : ScriptableObject
{
    public Dictionary<PlayerAppearance, EnemyPool> enemyPools;

    public GameObject GetFromPool(PlayerAppearance enemyType, Vector3 position)
    {
        var pool = enemyPools[enemyType].enemySpawning;

        foreach (var p in pool)
        {
            if (p.Item2.parentSpawner.hasAlreadySpawned) pool.Remove(p);
        }

        var spawned = pool.FirstOrDefault();

        spawned.Item2.transform.position = position;

        return spawned.Item2.gameObject;
    }

    public Type GetEnemyType(PlayerAppearance enemyClass)
    {
        Type type = default;

        switch (enemyClass)
        {
            case PlayerAppearance.Deer: type = typeof(DeerSkills); break;
            case PlayerAppearance.Harpy: type = typeof(HarpySkills); break;

            default: Debug.Log($"The entity you are referencing isn't an enemy: " + type); break;
        }

        return type;
    }
}
