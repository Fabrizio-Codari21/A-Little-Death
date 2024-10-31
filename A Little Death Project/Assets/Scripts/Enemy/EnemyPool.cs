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
    public List<Tuple<Type, Spawnable>> enemySpawning;
    Type _type;

    void Awake()
    {
        enemySpawning = FilterByClass(allEnemies, enemyType, out _type);
        enemyManager.enemyPools.Add(enemyType, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Tuple<Type, Spawnable>> FilterByClass(List<Spawnable> enemies, PlayerAppearance enemyClass, out Type enemyType)
    {
        Type type = enemyManager.GetEnemyType(enemyClass);

        List<Tuple<Type, Spawnable>> enemySpawning = new();

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
                enemySpawning.Add(new Tuple<Type, Spawnable>(t, enemy));
            }
        }

        enemyType = type;
        return enemySpawning;
    }

}
