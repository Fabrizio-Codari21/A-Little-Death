using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThaniaMovement : EntityMovement
{
    public Rigidbody2D rb;
    private float direction;
    public float moveSpeed = 7f;
    public bool isFacingRight = true;
    public bool isDashing;
    public AnimationManager anim;
    public Transform checkpointOne;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public Vector2 wallCheckSize;

    [HideInInspector] public bool touchingGround;
    [HideInInspector] public bool touchingWall;

    private void Awake()
    {

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(groundCheck) touchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if(wallCheck) touchingWall = Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0, groundLayer);
         
        if (isDashing == false && canMove)
        {
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
            direction = Input.GetAxisRaw("Horizontal");
            Flip();
        }

        if (this.Inputs(MyInputs.Secret2)) transform.position = checkpointOne.position;
        if (this.Inputs(MyInputs.Secret1)) Checkpoints.checkPoint = default;

        if (this.Inputs(MyInputs.Secret3)) 
        {
            var health = GetComponent<ThaniaHealth>();

            var save = (Checkpoints.checkPoint != new Vector3(health.startPos.x, health.startPos.y)) 
                       ? (SceneManager.GetActiveScene().buildIndex * 2) - 1 
                       : (SceneManager.GetActiveScene().buildIndex * 2) - 2;

            var saveInfo = new SaveInfo(save, 
                                        Checkpoints.savedPos, 
                                        health.currentHealth);

            if(saveInfo.sceneToKeep == string.Empty) { print("hubo un problema"); }

            this.SaveGame(saveInfo, SaveManager.currentSave);
        }

        if (this.Inputs(MyInputs.Secret4))
        {
            this.LoadGame(1);
        }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(wallCheck) Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
    }
}
