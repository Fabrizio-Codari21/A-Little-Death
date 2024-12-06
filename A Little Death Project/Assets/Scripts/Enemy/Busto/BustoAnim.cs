using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustoAnim : MonoBehaviour
{
    public BustoMovement movement;
    public Animator anim;

    void startRoll()
    {
        movement.canRoll = true;
    } 
    
    void stopRoll()
    {
        movement.canRoll = false;
    }

    private void ResetAnim()
    {
        Debug.Log("Restart");
        anim.ResetTrigger("Restart");
    }
}
