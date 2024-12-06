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
    public bool hasActivated = false;

    public Animator animator;

    public bool canRoll;

    Collider2D myColliderA;
    Collider2D myColliderB;
    private void Awake()
    {
        canMove = false;
        myColliderA = GetComponent<CircleCollider2D>();
        myColliderB = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        touchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        //Collider2D foundWall = Physics2D.OverlapCircle(wallCheck.position, 0.8f, groundLayer);
        //touchingWall = (foundWall != myColliderA && foundWall != myColliderB && foundWall != null);

        touchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.5f, groundLayer);

        grounded = Physics2D.OverlapBox(transform.position, boxSize, 0, groundedLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, LOS, 0, playerLayer);
        
        cooldown -= Time.deltaTime;

        if (canMove)
        {
            if ((!canSeePlayer && !rolling) || cooldown > 0)
            {
                Debug.Log("PATROLLING");
                Patrol();
            }
            else if ((canSeePlayer || rolling) && grounded && cooldown < 0)
            {
                if (!rolling)
                {
                    FlipToPlayer();
                    rolling = true;
                    bustHealth.immune = true;
                    animator.SetTrigger("Attack");
                }
                if (canRoll) { RollAttack(); }
            }
            else
            {
                rolling = false;
            }
        }
        else if(canSeePlayer && hasActivated == false) 
        {
            //ANIMACION DE AWAKE
            animator.SetTrigger("Activated");
            canMove = true;
            hasActivated = true;
        }
    }

    public override void FixedUpdate()
    {
        
    }

    private void RollAttack()
    {
        //ANIMACION DE ROLL
        //Aca pondria el codigo del ataque...
        Debug.Log("ATACK");
        if (!touchingGround || touchingWall)
        {
            Debug.Log("ATACK2");

            //ANIMACION DE LEVANTARSE
            animator.SetTrigger("Stun");
            //Esto en animacion
            StopRoll();

        }
        
        rb.velocity = new Vector2(rollSpeed * moveDirection, rb.velocity.y); //else rb.velocity = Vector2.zero;
    }

    private void StopRoll()
    {
        cooldown = 2.5f;
        player.audioManager.wallHit.Play();
        canSeePlayer = false;
        canMove = false;
        rolling = false;
        bustHealth.immune = false;

        this.WaitAndThen(timeToWait: 1.33f, () =>
        {
            if (bustHealth.currentHealth > 0)
            {
                canMove = true;
                Flip();
            }
            bustHealth.immune = true;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(wallCheck.position, 0.5f);
    }
}

