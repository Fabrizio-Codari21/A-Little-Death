using DragonBones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] UnityArmatureComponent animations;
    public bool jumped = false;
    public bool attacked = false;
    [SerializeField] ThaniaMovement thania;
    [SerializeField] JumpManager thaniaJ;
    bool playingIdle;
    bool playingWalk;
    bool playingRun;
    public bool attacking;

    private void Start()
    {
        thania = GetComponent<ThaniaMovement>();
        thaniaJ = GetComponent<JumpManager>();
    }

    private void Update()
    {
        if (attacked)
        {
            attacked = false;
            animations.animation.Stop();
            Attack();
        }
        else if (jumped == true)
        {
            jumped = false;
            animations.animation.Stop();
            Jump();
        }
        else
        {
            if (thaniaJ.grounded && attacking == false)
            {
                checkSpeed(thania.rb.velocity.x);
            }
        }
    }
    public void checkSpeed(float velocity)
    {
        if (velocity == 0)
        {
            if (playingIdle == false)
            {
                turnAllOff();
                playingIdle = true;
                Debug.Log("Idle");
                Idle();
            }
        }
        else if (velocity < thania.moveSpeed)
        {
            if (playingWalk == false)
            {
                turnAllOff();
                playingWalk = true;
                Debug.Log("Walk");
                Walk();
            }
        }
        else
        {
            if (playingRun == false)
            {
                turnAllOff();
                playingRun = true;
                Debug.Log("Run");
                Run();
            }
        }
    }

    public void turnAllOff()
    {
        playingIdle = false;
        playingRun = false;
        playingWalk = false;
    }

    public void Jump()
    {
        animations.animation.timeScale = 1f;
        animations.animation.Play("Jumping_Thania_Ver2", 1);
    }

    public void Walk()
    {
        animations.animation.timeScale = 1f;
        animations.animation.FadeIn("Walking_Thania_Ver2", 0.25f, -1);
    }

    public void Run()
    {
        animations.animation.timeScale = 1f;
        animations.animation.FadeIn("Running_Thania", 0.25f, -1);
    }

    public void Attack()
    {
        attacking = true;
        turnAllOff();
        animations.animation.timeScale = 3;
        animations.animation.Play("Attack_Thania", 1);
    }

    public void Idle()
    {
        animations.animation.timeScale = 1f;
        animations.animation.FadeIn("Iddle_Thania", 0.25f, -1);
    }

    public void StopJump()
    {
        animations.animation.timeScale = 1f;
        animations.animation.Stop("Jumping_Thania_Ver2");
        Debug.Log(thania.rb.velocity.x);
        checkSpeed(thania.rb.velocity.x);
    } 
    
    public void StopAttack()
    {
        attacking = false;
        animations.animation.timeScale = 1f;
        Debug.Log("Stop Attack");
        checkSpeed(thania.rb.velocity.x);
    }
}
