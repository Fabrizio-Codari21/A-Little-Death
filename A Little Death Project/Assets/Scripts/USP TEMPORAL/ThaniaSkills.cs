using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThaniaSkills : MonoBehaviour
{

    public CharacterSkillSet mySkills;

    void Awake()
    {
        var move = GetComponent<ThaniaMovement>();

        mySkills.primaryExecute = () =>
        {
            if (Time.time > mySkills.primaryExecTime && mySkills.primaryHasExecuted == false)
            {
                mySkills.primaryHasExecuted = true;
                Debug.Log("Attacked");
                move.anim.attacked = true;
                //StartCoroutine(move.ShowHitbox());
                mySkills.primaryExecTime = Time.time + mySkills.primaryCooldown;
            }
        };

        mySkills.secondaryExecute = () =>
        {

        };
    }

}
