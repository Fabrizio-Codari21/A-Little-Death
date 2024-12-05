using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FreeRoamMovement : EntityMovement
{
    [SerializeField] float speed;
    protected float moveDirection = -1;
    public Transform groundCheck;
    public Transform wallCheck;
    [SerializeField] public LayerMask groundLayer;
    public bool touchingGround;
    public bool touchingWall;
    public Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        touchingGround = Physics2D.OverlapCircle(groundCheck.position,0.2f,groundLayer);
        touchingWall = Physics2D.OverlapCircle(wallCheck.position,0.2f,groundLayer);
        Patrol();
    }
    
    public void Patrol()
    {
        if(!touchingGround || touchingWall)
        {
            Flip();
        }
        if (canMove) rb.velocity = new Vector2(speed * moveDirection, rb.velocity.y); else rb.velocity = Vector2.zero;
    }

    public void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        Gizmos.DrawWireSphere(wallCheck.position, 0.2f);
    }
}
