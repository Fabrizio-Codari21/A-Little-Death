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
            manager.thaniaMovement.rb.gravityScale = 20;
            manager.SetColliderAction(mySkills, true);
            manager.thaniaHealth.immune = true;

            manager.ExecuteUntilTrue(() => manager.jumpManager.grounded, () =>
            {
                var hit = Physics2D.CircleCast(mySkills.primaryOrigin.position,
                                               mySkills.primaryDistance,
                                               mySkills.primaryOrigin.position);

                Debug.Log("activa habilidad");

                if (hit.collider.gameObject.layer == mySkills.primaryValidLayer)
                {
                    hit.collider.GetComponent<BreakableFloor>().Break(manager.gameObject);

                    manager.jumpManager.anim.animator.SetTrigger("Stun");
                    Debug.Log($"{hit.collider.gameObject.name} was broken by {manager.sk.skills[1].skillType}");
                } 
            });

            manager.ExecuteAfterTrue(() => manager.jumpManager.grounded, () =>
            {
                //Lo que sea que haga el bicho cuando es de piedra
                Debug.Log("esta en el piso");
                //manager.thaniaMovement.canMove = false;
                //manager.jumpManager.canMove = false;

                manager.WaitAndThen(timeToWait: mySkills.primaryCooldown, () =>
                {
                    // desactivar la habilidad
                    Debug.Log("se desactivo");
                    manager.thaniaMovement.rb.gravityScale = 2;
                    manager.thaniaHealth.immune = false;
                    manager.SetColliderAction(mySkills, false);

                    //manager.thaniaMovement.canMove = true;
                    //manager.jumpManager.canMove = true;
                },
                cancelCondition: () => false);

            });
        };

        mySkills.secondaryExecute = (manager) =>
        {
            manager.thaniaMovement.canMove = false;
            manager.thaniaMovement.rb.gravityScale = 5;

            manager.ExecuteUntil(timeLimit: mySkills.secondaryCooldown, () =>
            {              
                var dir = (manager.thaniaMovement.isFacingRight) ? Vector2.right : Vector2.left;

                manager.thaniaMovement.rb.velocity = dir * (mySkills.secondaryEffectAmount * 100) * Time.fixedDeltaTime;
                if (!manager.jumpManager.grounded) manager.thaniaMovement.rb.velocity += (Vector2.down * 0.98f * 5);

            }, cancelCondition: () => /*si colisiona con una pared*/ false);

            manager.WaitAndThen(timeToWait: mySkills.secondaryCooldown, () =>
            {
                manager.thaniaMovement.canMove = true;
                manager.thaniaMovement.rb.gravityScale = 2;
            },
            cancelCondition: () => false);
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
