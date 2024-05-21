using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpManager : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    public float jumpForce = 14f;
    public bool dJump;
    [SerializeField] private int currentJump = 1;
    private bool dJumped;
    public AnimationManager anim;
    public bool grounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (dJump == false)
        {
            if (grounded)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    anim.jumped = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
            }
        }
        else
        {
            if (dJumped == true && grounded)
            {
                //anim.jumped = true;
                dJumped = false;
                currentJump = 0;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (grounded && dJumped == false)
                {
                    anim.jumped = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    currentJump++;
                }
                else if (!grounded && dJumped == false)
                {
                    anim.jumped = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    currentJump++;
                    dJumped = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.5f);
    }
}
