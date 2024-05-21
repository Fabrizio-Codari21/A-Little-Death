using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackList : MonoBehaviour
{
    public string classType;
    public float cooldown;
    public float nextFireTime;
    public bool attacked;
    public abstract void Attack();
}
