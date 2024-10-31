using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public EnemyManager enemyManager;
    
    public PlayerAppearance enemyType;
    public List<Spawnable> allEnemies;
    public Dictionary<ISkillDefiner, Spawnable> enemySpawning;
    Type _type;

    void Awake()
    {
        enemySpawning = FilterByClass(allEnemies, enemyType, out _type);
        enemyManager.enemyPools.Add(enemyType, enemySpawning);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Dictionary<ISkillDefiner, Spawnable> FilterByClass(List<Spawnable> enemies, PlayerAppearance enemyClass, out Type enemyType)
    {
        Type type = GetEnemyType(enemyClass);        

        Dictionary<ISkillDefiner, Spawnable> enemySpawning = new();

        foreach (var enemy in enemies)
        {
            var skill = enemy.gameObject.GetComponent<ISkillDefiner>();
            var t = skill.GetType();

            if (t != type)
            {
                enemies.Remove(enemy);
            }
            else
            {
                enemySpawning.Add(skill, enemy);
            }
        }

        enemyType = type;
        return enemySpawning;
    }

    public static Type GetEnemyType(PlayerAppearance enemyClass)
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
