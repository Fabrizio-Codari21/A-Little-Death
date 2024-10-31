using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "ScriptableObjects/EnemyManager")]
public class EnemyManager : ScriptableObject
{
    public Dictionary<PlayerAppearance, Dictionary<ISkillDefiner, Spawnable>> enemyPools;
}
