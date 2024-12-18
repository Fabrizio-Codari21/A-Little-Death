using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustSkills : MonoBehaviour, ISkillDefiner
{
    public CharacterSkillSet mySkills;
    
    public void DefineSkills(CharacterSkillSet mySkills)
    {
        mySkills.primaryExecute = (manager) =>
        {
            manager.thaniaMovement.rb.gravityScale = 30;
            manager.SetColliderAction(mySkills, true, SkillSlot.primary);
            manager.thaniaHealth.immune = true;
            if (!manager.jumpManager.grounded) { manager.isBreaking = true; }
            manager.thaniaMovement.anim.animator.SetTrigger("RockStart");
            mySkills.primaryHasExecuted = true;
            manager.thaniaHealth.audioManager.groundDrop.Play();

            manager.ExecuteUntilTrue(() => manager.jumpManager.grounded, () =>
            {
                var hit = Physics2D.CircleCast(mySkills.primaryOrigin.position,
                                               mySkills.primaryDistance,
                                               mySkills.primaryOrigin.position);

                Debug.Log("activa habilidad");

                if (hit.collider.gameObject.layer == mySkills.primaryValidLayer)
                {
                    hit.collider.GetComponent<BreakableFloor>().Break(manager.gameObject);
                    manager.thaniaHealth.audioManager.wallBreak.Play();

                    manager.jumpManager.anim.animator.SetTrigger("Stun");
                    Debug.Log($"{hit.collider.gameObject.name} was broken by {manager.sk.skills[1].skillType}");
                } 
            });

            manager.ExecuteAfterTrue(() => manager.jumpManager.grounded, () =>
            {
                //Lo que sea que haga el bicho cuando es de piedra
                Debug.Log("esta en el piso");
                manager.thaniaMovement.StopMoving();
                manager.jumpManager.canMove = false;
                manager.thaniaHealth.audioManager.groundPound.Play();

                manager.WaitAndThen(timeToWait: mySkills.primaryCooldown, () =>
                {
                    // desactivar la habilidad
                    manager.thaniaMovement.anim.animator.SetTrigger("RockEnd");
                    Debug.Log("se desactivo");


                    this.WaitAndThen(timeToWait: 0.25f, () =>
                    {
                        manager.thaniaMovement.rb.gravityScale = 2;
                        manager.thaniaHealth.immune = false;
                        manager.SetColliderAction(mySkills, false);
                        manager.isBreaking = false;

                        manager.thaniaMovement.canMove = true;
                        manager.jumpManager.canMove = true;

                        mySkills.primaryHasExecuted = false;
                    },
                    cancelCondition: () => false);

                },
                cancelCondition: () => false);

            });
        };

        mySkills.secondaryExecute = (manager) =>
        {
            var sprite = manager.sprites[manager._currentSprite];

            if (!manager.jumpManager.anim.jumped || mySkills.secondaryExecTime <= 0)
            {
                mySkills.secondaryExecTime++;
                manager.thaniaMovement.anim.animator.SetTrigger("AttackTrigger");
                manager.thaniaMovement.StopMoving();
                manager.thaniaMovement.rb.gravityScale = 4;
                mySkills.secondaryHasExecuted = true;

                if (sprite.normalCollider && sprite.altCollider)
                {
                    sprite.normalCollider.enabled = false;
                    sprite.altCollider.enabled = true;
                }

                manager.SetColliderAction(mySkills, true, SkillSlot.primary);

                manager.ExecuteUntil(timeLimit: mySkills.secondaryCooldown, () =>
                {
                    var dir = (manager.thaniaMovement.isFacingRight) ? 1 : -1;
                    //var jump = (manager.jumpManager.anim.jumped)
                    //            ? new Vector2(0, manager.jumpManager.jumpForce)
                    //            : Vector2.zero;

                    manager.thaniaMovement.rb.velocity = new Vector2(dir * (mySkills.secondaryEffectAmount * 100) * Time.fixedDeltaTime, 
                                                                     manager.thaniaMovement.rb.velocity.y);

                    //manager.thaniaMovement.rb.velocity += (Vector2.down * 0.98f * manager.thaniaMovement.rb.gravityScale);


                }, cancelCondition: 
                () => manager.ExecuteIfCancelled(manager.thaniaMovement.touchingWall 
                                                || (mySkills.primaryHasExecuted 
                                                && manager.jumpManager.grounded), () =>
                {
                    Debug.Log("se choco"); 
                    manager.thaniaHealth.audioManager.wallHit.Play(); 
                    ReturnToNormal();
                }));

               /* manager.WaitAndThen(timeToWait: mySkills.secondaryCooldown, () =>
                {
                    Debug.Log("TIMESUP");
                    ReturnToNormal();
                },
                cancelCondition: () => manager.thaniaMovement.touchingWall
                                                || mySkills.secondaryHasExecuted
                                                );*/
            }

            void ReturnToNormal()
            {
                if (mySkills.primaryHasExecuted)
                {
                    manager.thaniaMovement.anim.animator.SetTrigger("FinishAttack");
                }
                else
                {
                    manager.thaniaMovement.anim.animator.SetTrigger("PARALOCO");
                }
                //manager.thaniaMovement.canMove = true;
                manager.thaniaMovement.rb.gravityScale = 2;

                if (sprite.normalCollider && sprite.altCollider)
                {
                    sprite.normalCollider.enabled = true;
                    sprite.altCollider.enabled = false;
                }

                manager.SetColliderAction(mySkills, false, SkillSlot.primary);
                mySkills.secondaryHasExecuted = false;

                manager.WaitAndThen(timeToWait: 1f, () =>
                         {
                             mySkills.secondaryExecTime = 0;
                         },
                         cancelCondition: () => false);
            }
        };
    }

    void Awake()
    {
        DefineSkills(mySkills);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(mySkills) Gizmos.DrawWireSphere(mySkills.primaryOrigin.position, mySkills.primaryDistance);
    }
}
