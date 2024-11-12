using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BustoMovement : FreeRoamMovement
{
    [SerializeField] bool grounded;
    [SerializeField] Transform groundDetect;
    [SerializeField] Vector2 boxSize; 
    [SerializeField] public LayerMask groundedLayer;

    [SerializeField] bool canSeePlayer = false;
    [SerializeField] Vector2 LOS;
    [SerializeField] public LayerMask playerLayer;

    [SerializeField] bool rolling;
    public float cooldown;
    public ThaniaHealth player;

    [SerializeField] float rollSpeed;


    public override void FixedUpdate()
    {
        touchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        touchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);
        grounded = Physics2D.OverlapBox(groundDetect.position, boxSize, 0, groundedLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, LOS, 0, playerLayer);

        if (!canSeePlayer)
        {
            base.Patrol();
            rolling = false;
        }
        else if (canSeePlayer && grounded)
        {
            if (!rolling)
            {
                FlipToPlayer();
                rolling = true;
            }
            RollAttack();
        }
    }

    private void RollAttack()
    {
        //Aca pondria el codigo del ataque...
        Debug.Log("ATACK");
        if (!touchingGround || touchingWall)
        {
            StopRoll();
            Flip();
        }
        
        rb.velocity = new Vector2(rollSpeed * moveDirection, rb.velocity.y); //else rb.velocity = Vector2.zero;
    }

    private void StopRoll()
    {
        canSeePlayer = false;
        rolling = false;
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
}

