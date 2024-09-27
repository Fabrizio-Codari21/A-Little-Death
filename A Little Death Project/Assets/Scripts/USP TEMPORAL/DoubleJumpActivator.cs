using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpActivator : MonoBehaviour
{
    [SerializeField] bool harpy;
    [SerializeField] JumpManager jMan;

    private void OnEnable()
    {
        if(harpy) { jMan.dJump = true; }
        else { jMan.dJump = false; }
    }
}
