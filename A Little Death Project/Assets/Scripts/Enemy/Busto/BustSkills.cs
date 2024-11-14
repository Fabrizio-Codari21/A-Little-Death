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

            manager.ExecuteUntilTrue(() => manager.jumpManager.grounded, () =>
            {
                var hit = Physics2D.CircleCast(mySkills.primaryOrigin.position,
                                               mySkills.primaryDistance,
                                               mySkills.primaryOrigin.position);

                Debug.Log("activa habilidad");

                if (hit.collider.gameObject.layer == mySkills.primaryValidLayer)
                {
                    hit.collider.GetComponent<BreakableDoor>().Break(manager.gameObject);

                    manager.jumpManager.anim.animator.SetTrigger("Stun");
                    Debug.Log($"{hit.collider.gameObject.name} was broken by {manager.sk.skills[1].skillType}");
                } 
            });

            manager.ExecuteAfterTrue(() => manager.jumpManager.grounded, () =>
            {
                //Lo que sea que haga el bicho cuando es de piedra
                Debug.Log("esta en el piso");

                this.WaitAndThen(timeToWait: mySkills.primaryCooldown, () =>
                {
                    // desactivar la habilidad
                    Debug.Log("se desactivo");
                    manager.thaniaMovement.rb.gravityScale = 2;
                },
                cancelCondition: () => false);

            });
        };

        mySkills.secondaryExecute = (manager) =>
        {

        };
    }

    void Awake()
    {
        DefineSkills(mySkills);
    }
}
