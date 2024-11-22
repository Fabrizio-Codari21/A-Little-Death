using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CADA HABILIDAD SE REPRESENTA EN UNA DE ESTAS CLASES
public class SkillList
{
    //public string classType;
    public SkillType skillType;
    public SkillSlot skillSlot;
    public float distance;
    public float effectAmount;
    public LayerMask validLayer;
    public Transform origin;
    public GameObject spawn;
    public float cooldown;
    public float nextFireTime;
    public bool executed;
    public ColliderAction colliderAction;

    public Action<PlayerSkillManager> Execute;
    public int input;

    // Construimos la nueva habilidad en base al struct que le pasemos
    public SkillList(SkillSet skillSet)
    {
        skillType = skillSet.skillType;
        skillSlot = skillSet.skillSlot;
        cooldown = skillSet.cooldown;
        distance = skillSet.distance;
        effectAmount = skillSet.effectAmount;
        validLayer = skillSet.validLayer;
        origin = skillSet.origin;
        spawn = skillSet.spawn;
        nextFireTime = skillSet.nextFireTime;
        Execute = skillSet.Execute;
        colliderAction = skillSet.colliderAction;

        // El input a usar depende de su valor de skillSlot
        input = (skillSlot == SkillSlot.primary) ? 0 : 1;
    }
}
