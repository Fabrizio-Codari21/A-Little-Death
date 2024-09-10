using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nos permite asignar las habilidades de cada personaje desde el inspector.
public class CharacterSkillSet : MonoBehaviour
{
    [Header("PRIMARY SKILL")]
    public ENUMS.SkillType primarySkillType;
    public float primaryCooldown;
    [HideInInspector] public float primaryExecTime;
    [HideInInspector] public bool primaryHasExecuted;
    public Action primaryExecute;
    
    [Space(20), Header("SECONDARY SKILL")]
    public ENUMS.SkillType secondarySkillType;
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
