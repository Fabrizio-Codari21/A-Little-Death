using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contiene las variables de la habilidad de cada personaje
public struct SkillSet
{
    public ENUMS.SkillType skillType;
    public ENUMS.SkillSlot skillSlot;
    public float cooldown;
    public float nextFireTime;
    public bool hasExecuted;

    public Action Execute;
}
