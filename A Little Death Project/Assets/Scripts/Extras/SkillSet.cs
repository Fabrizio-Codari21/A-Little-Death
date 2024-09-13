using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contiene las variables de la habilidad de cada personaje
public struct SkillSet
{
    public SkillType skillType;
    public SkillSlot skillSlot;
    public float range;
    public float effectAmount;
    public LayerMask validLayer;
    public float cooldown;
    public float nextFireTime;
    public bool hasExecuted;

    public Action Execute;
}
