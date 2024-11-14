using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "ScriptableObjects/EnemyManager")]
public class EnemyManager : ScriptableObject
{
    public Dictionary<PlayerAppearance, EnemyPool> enemyPools = new();

    public GameObject GetFromPool(CharacterSkillSet enemyType, Vector3 position, Quaternion rotation, bool isSpawning = true)
    {
        var pool = enemyPools[enemyType.creatureAppearance].enemySpawnables;
        List<Tuple<Type, Spawnable>> newPool = new();

        foreach (var p in pool)
        {
            if (p.Item2.parentSpawner.hasAlreadySpawned) newPool.Add(p);
        }

        var spawned = newPool.Where(x => x.Item2.gameObject == enemyType.gameObject).FirstOrDefault();

        if (spawned == default) Debug.Log("There are no enemies left to spawn.");



        if (isSpawning)
        {
            spawned.Item2.gameObject.transform.parent.position = position;
            spawned.Item2.transform.parent.rotation = rotation;
        }
        else
        {
            if (spawned.Item1 == typeof(HarpySkills))
            {
                spawned.Item2.GetComponentInChildren<WingsAttackDetection>().attacking = false;
                spawned.Item2.transform.parent.position = enemyPools[enemyType.creatureAppearance].transform.position;
                // spawned.Item2.transform.position = enemyPools[enemyType.creatureAppearance].transform.position;
                spawned.Item2.transform.localPosition = Vector3.zero;
            }
            else
            {

                spawned.Item2.transform.parent.position = enemyPools[enemyType.creatureAppearance].transform.position + new Vector3(0, 5, 0);
                spawned.Item2.transform.position = enemyPools[enemyType.creatureAppearance].transform.position + new Vector3(0, 5, 0);
            }
        }

        
        return spawned.Item2.gameObject;
    }

    public Type GetEnemyType(PlayerAppearance enemyClass)
    {
        Type type = default;

        switch (enemyClass)
        {
            case PlayerAppearance.Deer: type = typeof(DeerSkills); break;
            case PlayerAppearance.Harpy: type = typeof(HarpySkills); break;
            case PlayerAppearance.Bust: type = typeof(BustSkills); break;

            default: Debug.Log($"The entity you are referencing isn't an enemy: " + type); break;
        }

        return type;
    }
}
