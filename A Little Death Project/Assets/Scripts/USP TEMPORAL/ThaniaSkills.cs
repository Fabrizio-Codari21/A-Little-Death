using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Hay que hacer una de estas por cada tipo de criatura con habilidades (todas heredando de ISkillDefiner)
// y asignarle el primaryExecute y secondaryExecute de cada una para saber que hace la habilidad.
public class ThaniaSkills : MonoBehaviour, ISkillDefiner
{
    public PlayerSkillManager skillManager;
    public CharacterSkillSet mySkills;
    public ThaniaMovement movement;
    [SerializeField] GameObject cartelTutorial;

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

                PossessableHealth damageable = hits.collider.gameObject.GetComponent<PossessableHealth>();

                if (damageable != null)
                {
                    var victim = hits.collider.gameObject.GetComponent<CharacterSkillSet>();
                    var dead = damageable.DamagePossessable((int)mySkills.primaryEffectAmount, victim, skillManager, cartelTutorial);
                    if (dead)
                    {
                        Debug.Log("Aca");                       
                        //skillManager.Possess(victim, victim.creatureAppearance);
                    }
                    else print("no murio");
                }
            }
        }

        //transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        mySkills.primaryHasExecuted = false;
        movement.anim.attacked = false;
        //transform.GetChild(2).gameObject.SetActive(false);
    }

    public void DefineSkills(CharacterSkillSet mySkills)
    {
        mySkills.primaryExecute = (manager) =>
        {
            if (Time.time > mySkills.primaryExecTime && mySkills.primaryHasExecuted == false)
            {
                mySkills.primaryHasExecuted = true;
                Debug.Log("Attacked");
                movement.anim.attacked = true;
                manager.StartCoroutine(AttackEnemy());
                mySkills.primaryExecTime = Time.time + mySkills.primaryCooldown;
            }
        };

        mySkills.secondaryExecute = (manager) =>
        {

        };
    }
}
