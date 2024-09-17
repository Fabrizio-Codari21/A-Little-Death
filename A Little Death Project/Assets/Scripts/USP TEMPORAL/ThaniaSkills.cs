using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThaniaSkills : MonoBehaviour, ISkillDefiner
{
    public PlayerSkillManager skillManager;
    public CharacterSkillSet mySkills;
    public ThaniaMovement movement;

    void Awake()
    {
        var move = GetComponent<ThaniaMovement>();

        DefineSkills(mySkills);
    }

    private IEnumerator AttackEnemy()
    {
        if (mySkills.primaryHasExecuted)
        {
            var hits = Physics2D.CircleCast(mySkills.primaryOrigin.position, 
                                            mySkills.primaryDistance, 
                                            transform.right, 
                                            0f, 
                                            mySkills.primaryValidLayer);

            if (hits != false)
            {
                Debug.Log("Dealt Damage");

                IDamageable damageable = hits.collider.gameObject.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    var dead = damageable.Damage((int)mySkills.primaryEffectAmount);
                    if (dead)
                    {
                        Debug.Log("Aca");
                        var newSkills = hits.collider.gameObject.GetComponent<CharacterSkillSet>();
                        print(skillManager.BuildSkillSet(newSkills.primarySkill, newSkills.secondarySkill));
                    }
                    else print("no murio");
                }
            }
        }

        //transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        mySkills.primaryHasExecuted = false;
        movement.anim.attacked = false;
        //transform.GetChild(2).gameObject.SetActive(false);
    }

    public void DefineSkills(CharacterSkillSet mySkills)
    {
        mySkills.primaryExecute = () =>
        {
            if (Time.time > mySkills.primaryExecTime && mySkills.primaryHasExecuted == false)
            {
                mySkills.primaryHasExecuted = true;
                Debug.Log("Attacked");
                movement.anim.attacked = true;
                StartCoroutine(AttackEnemy());
                mySkills.primaryExecTime = Time.time + mySkills.primaryCooldown;
            }
        };

        mySkills.secondaryExecute = () =>
        {

        };
    }
}
