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
    public GorgonHealth gorgonHealth;
    public GameObject aimPivot;
    public GorgonProjectile projectile;
    public float shootingPower;

    public Animator animator;

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

        if(cooldown > 0 && !spitting) cooldown -= Time.deltaTime;

        if (cooldown <= 0 && canMove)
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
                if (cooldown <= 0)
                {
                    Spit();
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

    bool spitting = false;
    public void Spit()
    {
        animator.SetTrigger("Aim");
        cooldown = 1;
        spitting = true;
        print("spitting: " + cooldown);

        var dir = facingRight
                  ? transform.right
                  : transform.right * -1;

        //aimPivot.SetActive(true);

        this.ExecuteUntil(timeLimit: 1f, () =>
        {           
            dir = player.transform.position - transform.position;
            aimPivot.transform.LookAt(player.transform.position);
            aimPivot.transform.Rotate(new Vector3(0,-90,0));

        }, cancelCondition: () => this.ExecuteIfCancelled(gorgonHealth.currentHealth <= 0, () =>
        {
            spitting = false;
            aimPivot.SetActive(false);
            cooldown = 99999;
        }));



        this.WaitAndThen(timeToWait: 1.4f, () =>
        {
            animator.SetTrigger("Attack");

            var proj = Instantiate(projectile.gameObject,
                                   aimPivot.transform.position,
                                   Quaternion.identity).
                                   GetComponent<GorgonProjectile>();

            if (proj != default)
            {
                /*if (facingRight)
                {
                    proj.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    proj.GetComponent<SpriteRenderer>().flipX = true; 
                }*/
                player.audioManager.spit.Play();
                proj.GetComponent<Rigidbody2D>().AddForce(dir * shootingPower * 25);
                proj.skillManager = player.GetComponent<PlayerSkillManager>();
                proj.OnImpact = () =>
                {
                    proj.skillManager.SetColliderAction(GetComponent<CharacterSkillSet>(), false, SkillSlot.primary);
                    Destroy(proj.gameObject, 0.02f);
                };
            }
            else Debug.Log("There is no projectile.");


            spitting = false;
            aimPivot.SetActive(false);

            //this.ExecuteUntil(timeLimit: 1f, () =>
            //{
            //    cooldown -= Time.fixedDeltaTime;
            //});

        }, cancelCondition: () => this.ExecuteIfCancelled(gorgonHealth.currentHealth <= 0, () =>
        {
            spitting = false;
            aimPivot.SetActive(false);
            cooldown = 99999;
        }));
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
