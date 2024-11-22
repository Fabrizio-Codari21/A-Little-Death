using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustoAnim : MonoBehaviour
{
    public BustoMovement movement;

    void startRoll()
    {
        movement.canRoll = true;
    } 
    
    void stopRoll()
    {
        movement.canRoll = false;
    }
}
