using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonMovement : FreeRoamMovement
{
    [SerializeField] bool canSeePlayer = false;
    [SerializeField] Vector2 LOS;
    [SerializeField] Vector2 LOSOffset;
    [SerializeField] public LayerMask playerLayer;

    public float cooldown;
    public ThaniaHealth player;
    public GameObject projectile;

    //public Animator animator;

    private void Update()
    {
        touchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        touchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);

        if (facingRight)
        {
            canSeePlayer = Physics2D.OverlapBox(transform.position + new Vector3(-LOSOffset.x, LOSOffset.y, 0), LOS, 0, playerLayer);
        }
        else if (!facingRight)
        {
            canSeePlayer = Physics2D.OverlapBox(transform.position + new Vector3(LOSOffset.x, LOSOffset.y, 0), LOS, 0, playerLayer);
        }

        cooldown -= Time.deltaTime;

        if (canMove)
        {
            if (!canSeePlayer)
            {
                Patrol();
            }
            else if (canSeePlayer)
            {
                Rigidbody2D body = GetComponent<Rigidbody2D>();
                body.velocity = Vector2.zero; 
                //FlipToPlayer();
                if (cooldown < 0)
                {
                    //Spit
                }
            }
        }
    }

    public override void FixedUpdate()
    {

    }

    void FlipToPlayer()
    {
        float distance = player.transform.position.x - transform.position.x;
        if (distance < 0 && facingRight)
        {
            base.Flip();
        }
        else if (distance > 0 && !facingRight)
        {
            base.Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (facingRight)
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(-LOSOffset.x, LOSOffset.y, 0), LOS);
        }
        else if(!facingRight) 
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(LOSOffset.x, LOSOffset.y, 0), LOS);

        }
    }
}
