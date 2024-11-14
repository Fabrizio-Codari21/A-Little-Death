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
    public BustHealth bustHealth;

    [SerializeField] float rollSpeed;

    private void Update()
    {
        touchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        touchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);
        grounded = Physics2D.OverlapBox(transform.position, boxSize, 0, groundedLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, LOS, 0, playerLayer);
        
        cooldown -= Time.deltaTime;

        if (canMove)
        {
            if ((!canSeePlayer && !rolling) /*|| cooldown > 0*/)
            {
                Patrol();
            }
            else if ((canSeePlayer || rolling) && grounded && cooldown < 0)
            {
                if (!rolling)
                {
                    FlipToPlayer();
                    rolling = true;
                    bustHealth.immune = true;
                }
                RollAttack();
            }
            else
            {
                rolling = false;
            }
        }
    }

    public override void FixedUpdate()
    {
        
    }

    private void RollAttack()
    {
        //Aca pondria el codigo del ataque...
        Debug.Log("ATACK");
        if (!touchingGround || touchingWall)
        {
            Debug.Log("ATACK2");

            //Animacion del final

            //Esto en animacion
            StopRoll();

        }
        
        rb.velocity = new Vector2(rollSpeed * moveDirection, rb.velocity.y); //else rb.velocity = Vector2.zero;
    }

    private void StopRoll()
    {
        cooldown = 2.5f;
        canSeePlayer = false;
        canMove = false;
        rolling = false;
        bustHealth.immune = false;
        Flip();

        this.WaitAndThen(timeToWait: 1f, () =>
        {
            canMove = true;
        },
        cancelCondition: () => false);
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

