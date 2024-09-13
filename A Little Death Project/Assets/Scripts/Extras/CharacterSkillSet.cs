using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nos permite asignar las habilidades de cada personaje desde el inspector.
public class CharacterSkillSet : MonoBehaviour
{
    [Header("PRIMARY SKILL")]
    public SkillType primarySkillType;
    [Tooltip("Represents distance values of your current action, such as range, radius, etc.")]
    public float primaryRange;
    [Tooltip("Represents the amount of something that your action applies, such as damage, points, etc.")]
    public float primaryEffectAmount;
    [Tooltip("Represents the layer that your action may affect.")]
    public LayerMask primaryValidLayer;
    [Tooltip("Represents the time it takes before your action can be executed again.")]
    public float primaryCooldown;
    [HideInInspector] public float primaryExecTime;
    [HideInInspector] public bool primaryHasExecuted;
    public Action primaryExecute;
    
    [Space(20), Header("SECONDARY SKILL")]   
    public SkillType secondarySkillType;
    [Tooltip("Represents distance values of your current action, such as range, radius, etc.")]
    public float secondaryRange;
    [Tooltip("Represents the amount of something that your action applies, such as damage, points, etc.")]
    public float secondaryEffectAmount;
    [Tooltip("Represents the layer that your action may affect.")]
    public LayerMask secondaryValidLayer;
    [Tooltip("Represents the time it takes before your action can be executed again.")]
    public float secondaryCooldown;
    [HideInInspector] public float secondaryExecTime;
    [HideInInspector] public bool secondaryHasExecuted;
    public Action secondaryExecute;

    // En estos structs se guarda el set de habilidades para que el jugador pueda acceder
    // a ellas facilmente.
    public SkillSet primarySkill = new();
    public SkillSet secondarySkill = new();

    private void Awake()
    {
        primarySkill = new SkillSet
        { 
            skillType = primarySkillType,
            cooldown = primaryCooldown,
            nextFireTime = primaryExecTime,
            hasExecuted = primaryHasExecuted,
            Execute = primaryExecute,
        };

        secondarySkill = new SkillSet
        {
            skillType = secondarySkillType,
            cooldown = secondaryCooldown,
            nextFireTime = secondaryExecTime,
            hasExecuted = secondaryHasExecuted,
            Execute = secondaryExecute,
        };
    }
}
