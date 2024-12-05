using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonPivot : MonoBehaviour
{
    [HideInInspector] public int counter;    
    
    public void AimingLoop()
    {
        counter++;
    }
}
