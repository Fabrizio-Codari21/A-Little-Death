using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WingsDash : DashList
{
    public JumpManager thania;

    private void Awake()
    {
        thania = GetComponent<JumpManager>();
        classType = "alas";
    }

    public override void Dash()
    {
        Debug.Log("Alas equipadas");
    }

    public void DJump(bool a)
    {
        thania.dJump = a;
    }
}
