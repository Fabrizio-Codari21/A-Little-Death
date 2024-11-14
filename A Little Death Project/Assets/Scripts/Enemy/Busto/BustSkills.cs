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

                if(hit.collider.gameObject.layer == mySkills.primaryValidLayer)
                {
                    hit.collider.GetComponent<BreakableDoor>().Break(manager.gameObject);

                    manager.jumpManager.anim.animator.SetTrigger("Stun");
                    Debug.Log($"{hit.collider.gameObject.name} was broken by {manager.sk.skills[1].skillType}");
                } 
            });

            manager.ExecuteAfterTrue(() => manager.jumpManager.grounded, () =>
            {
                //Lo que sea que haga el bicho cuando es de piedra
            });
        };

        mySkills.secondaryExecute = (manager) =>
        {

        };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
