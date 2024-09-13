using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillList
{
    //public string classType;
    public SkillType skillType;
    public SkillSlot skillSlot;
    public float range;
    public float effectAmount;
    public LayerMask validLayer;
    public float cooldown;
    public float nextFireTime;
    public bool executed;

    public Action Execute;
    public int input;

    // Construimos la nueva habilidad en base al struct que le pasemos
    public SkillList(SkillSet skillSet)
    {
        skillType = skillSet.skillType;
        skillSlot = skillSet.skillSlot;
        cooldown = skillSet.cooldown;
        range = skillSet.range;
        nextFireTime = skillSet.nextFireTime;
        Execute = skillSet.Execute;

        // El input a usar depende de su valor de skillSlot
        input = (skillSlot == SkillSlot.primary) ? 0 : 1;
    }
}
