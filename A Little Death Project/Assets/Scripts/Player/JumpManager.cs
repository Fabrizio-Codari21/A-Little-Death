using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpManager : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    public float jumpForce = 14f;
    public bool dJump;
    [SerializeField] private int currentJump = 1;
    private bool dJumped;
    public AnimationManager anim;
    public bool grounded = true;
    public ParticleSystem dust;

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
        if (dJump == false)
        {
            if (coyoteCounter > 0f && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
            {
                anim.jumped = true;
                jumpSound.Play();
                CreateDust();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W)) && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.75f);
            }
        }
        else
        {
            if (dJumped == true && grounded)
            {
                print("DOBLE SALTO");
                dJumped = false;
                currentJump = 0;
                coyoteCounter = 0;
            }

            if (coyoteCounter > 0f && dJumped == false && (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W)))
            {
                anim.jumped = true;
                jumpSound.Play();
                CreateDust();
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                currentJump++;
            }
            else if (!grounded && dJumped == false && Input.GetMouseButtonDown(1))
            {
                anim.jumped = true;
                jumpSound.Play();
                CreateDust();
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                currentJump++;
                dJumped = true;
            }

        }


        if (rb.velocity.y > 0f && (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W)))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.75f);
            coyoteCounter = 0;
        }
    }

    void CreateDust()
    {
        dust.Play();
    }
}

