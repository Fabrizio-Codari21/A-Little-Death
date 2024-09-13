using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThaniaSkills : MonoBehaviour
{
    public CharacterSkillSet mySkills;
    public ThaniaMovement movement;

    void Awake()
    {
        var move = GetComponent<ThaniaMovement>();

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

    private IEnumerator AttackEnemy()
    {
        if (mySkills.primaryHasExecuted)
        {
            var hits = Physics2D.CircleCast(transform.GetChild(2).transform.position, 
                                            mySkills.primaryRange, 
                                            transform.right, 
                                            0f, 
                                            mySkills.primaryValidLayer);

            if (hits != false)
            {
                Debug.Log("Dealt Damage");

                IDamageable damageable = hits.collider.gameObject.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.Damage((int)mySkills.primaryEffectAmount);
                }
            }
        }

        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        mySkills.primaryHasExecuted = false;
        movement.anim.attacked = false;
        transform.GetChild(2).gameObject.SetActive(false);
    }

}
