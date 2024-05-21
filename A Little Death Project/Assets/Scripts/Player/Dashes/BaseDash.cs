using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseDash : DashList
{
    private void Awake()
    {
        classType = "base";    
    }

    public override void Dash()
    {
        Debug.Log("DASH");
    }
}
