using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAttack : AttackList
{
    private void Awake()
    {
        classType = "deer";
    }

    public override void Attack()
    {
        Debug.Log("Ataque Venado");
    }
}
