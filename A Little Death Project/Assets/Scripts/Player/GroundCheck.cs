using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] 
    JumpManager manager;
    [SerializeField]

    private void Start()
    {
        manager = GetComponentInParent<JumpManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            manager.grounded = true;
                    manager.anim.StopJump();
}
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            manager.grounded = false;
        }
    }
}
