using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHability : HabilityList
{
    private void Awake()
    {
        classType = "base";
    }

    public override void Attack()
    {
        Debug.Log("HABILITY");
    }
}
