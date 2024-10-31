using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpManager : EntityMovement
{
    [SerializeField] private Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    public float jumpForce = 14f;
    public bool dJump;
    public int currentJump = 1;
    private bool dJumped;
    public AnimationManager anim;
    public bool grounded = true;
    public ParticleSystem dust;
    public ParticleSystem dJumpVFX;

    private float coyoteTime = 0.2f;
    private float coyoteCounter;

    [SerializeField] AudioSource jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (grounded)
            {
                coyoteCounter = coyoteTime;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }

            DoubleJump();
        }
    }

    public float doubleJumpForce;

    public void DoubleJump()
    {
        if (canMove)
        {
            if (dJump == false)
            {
                if (coyoteCounter > 0f && this.Inputs(MyInputs.MoveSkill))
                {
                    anim.jumped = true;
                    jumpSound.Play();
                    CreateDust();
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }

                //if (this.Inputs(MyInputs.MoveSkill) && rb.velocity.y > 0f)
                if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.75f);
                }
            }
            else
            {
                if (dJumped == true && grounded)
                {
                    dJumped = false;
                    currentJump = 0;
                    coyoteCounter = 0;
                }

                if (coyoteCounter > 0f && dJumped == false && this.Inputs(MyInputs.MoveSkill))
                {
                    anim.jumped = true;
                    jumpSound.Play();
                    CreateDust();
                    rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                    currentJump++;
                }
                else if (!grounded && dJumped == false && this.Inputs(MyInputs.SecondarySkill))
                {
                    Debug.Log("A");
                    anim.attackEnded = true;
                    anim.jumped = true;
                    jumpSound.Play();
                    CreateLines();
                    rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                    currentJump++;
                    dJumped = true;
                }

            }


            //if (rb.velocity.y > 0f && this.Inputs(MyInputs.MoveSkill))
            if (rb.velocity.y > 0f && Input.GetKeyUp(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.75f);
                coyoteCounter = 0;
            }
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    void CreateLines()
    {
        dJumpVFX.Play();
    }
}

