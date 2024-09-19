using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AttackManager : MonoBehaviour
{
    public AttackList[] attackTypes;
    public Dictionary<string, AttackList> attackType = new Dictionary<string, AttackList>(); 
    public HabilityList[] habilityTypes;
    public Dictionary<string, HabilityList> habilityType = new Dictionary<string, HabilityList>();
    public string primaryFire;
    public string secondaryFire;

    private void Start()
    {
        foreach (AttackList i in attackTypes) 
        {
            attackType.Add(i.classType, i);
        }
        foreach (HabilityList i in habilityTypes) 
        {
            habilityType.Add(i.classType, i);
        }
    }
}
