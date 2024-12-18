using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonAnimatorManager : MonoBehaviour
{
    public GorgonMovement movement;
    public Animator anim;

    public void StartAim()
    {
        movement.aimPivot.SetActive(true);
    }
    
    public void StopAim()
    {
        movement.aimPivot.SetActive(false);
    }

    private void ResetAnim()
    {
        Debug.Log("Restart");
        anim.ResetTrigger("Restart");
    }
}
