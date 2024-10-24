using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThaniaMovement : EntityMovement
{
    public Rigidbody2D rb;
    private float direction;
    public float moveSpeed = 7f;
    public bool isFacingRight = true;
    public bool isDashing;
    public AnimationManager anim;
    public Transform checkpointOne;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing == false && canMove)
        {
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
            direction = Input.GetAxisRaw("Horizontal");
            Flip();
        }

        if (this.Inputs(MyInputs.Secret2)) transform.position = checkpointOne.position;
        if (this.Inputs(MyInputs.Secret1)) Checkpoints.savedPos = default;
    }

    private void Flip()
    {
        if (Time.timeScale > 0)
        {
            if (isFacingRight && direction < 0f || !isFacingRight && direction > 0f)
            {
                Vector3 wheel = transform.GetChild(0).transform.localScale;
                Vector3 wheel2 = transform.GetChild(1).transform.localScale;
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                wheel.x *= -1f;
                wheel2.x *= -1f;
                transform.localScale = localScale;
                transform.GetChild(0).transform.localScale = wheel;
                transform.GetChild(1).transform.localScale = wheel2;
            }
        }
    }
}
