using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OlmecaAttack : FreeRoamMovement
{
    [SerializeField] float jumpHeight;
    GameObject thania;
    [SerializeField] Transform groundDetect;
    [SerializeField] Vector2 boxSize;
    [SerializeField] Vector2 LOS;
    [SerializeField] bool grounded;
    [SerializeField] public LayerMask player;
    bool canSeePlayer = false;
    float nextAttack;
    public float cooldown;
    [SerializeField] public LayerMask groundedLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thania = GameObject.FindWithTag("Player");
    }

    public override void FixedUpdate()
    {
        touchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        touchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);
        grounded = Physics2D.OverlapBox(groundDetect.position, boxSize, 0, groundedLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, LOS, 0, player);

        if (!canSeePlayer && grounded)
        {
            Patrol();
        }
        else if (canSeePlayer && grounded)
        {
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + cooldown;
                FlipToPlayer();
                JumpAttack();
            }
        }
        else if (!canSeePlayer && !grounded)
        {
            Debug.Log("floating");
        }
    }

    void JumpAttack()
    {
        float distance = thania.transform.position.x - transform.position.x;

        if(grounded)
        {
            rb.AddForce(new Vector2(distance, jumpHeight), ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundDetect.position, boxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, LOS);
    }

    void FlipToPlayer()
    {
        float distance = thania.transform.position.x - transform.position.x;
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
