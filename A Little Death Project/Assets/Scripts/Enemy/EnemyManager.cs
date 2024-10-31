using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerAppearance enemyType;
    public List<ISkillDefiner> allEnemies;

    void Awake()
    {
        allEnemies = FilterByClass(allEnemies, enemyType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<ISkillDefiner> FilterByClass(List<ISkillDefiner> enemies, PlayerAppearance enemyClass)
    {
        Type type = default;
        
        switch (enemyClass)
        {
            case PlayerAppearance.Deer: type = typeof(DeerSkills); break;
            case PlayerAppearance.Harpy: type = typeof(HarpySkills); break;

            default: print($"The entity you are referencing isn't an enemy: " + type); break;
        }

        foreach (var enemy in enemies)
        {
            var t = enemy.GetType();

            if(t != type)
            {
                enemies.Remove(enemy);
            }
        }

        return enemies;
    }
}
