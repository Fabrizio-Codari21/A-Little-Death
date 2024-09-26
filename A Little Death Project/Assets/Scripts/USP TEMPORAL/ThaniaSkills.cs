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
    //public GameObject hitbox;
    //public GameObject hitbox2;

    void Awake()
    {
        var move = GetComponent<ThaniaMovement>();

        DefineSkills(mySkills);
    }

    private IEnumerator AttackEnemy(PlayerSkillManager manager)
    {
        if (mySkills.primaryHasExecuted)
        {

            var hits = Physics2D.CircleCast(mySkills.primaryOrigin.position,
                                            mySkills.primaryDistance,
                                            transform.right,
                                            0f,
                                            mySkills.primaryValidLayer);
            

            if (hits != false && manager.GetColliderAction() == ColliderAction.Damage)
            {
                Debug.Log( $"{mySkills.primarySkillType} dealt {mySkills.primaryEffectAmount} damage");

                PossessableHealth damageable = hits.collider.gameObject.GetComponent<PossessableHealth>();

                if (damageable != null)
                {
                    var victim = hits.collider.gameObject.GetComponent<CharacterSkillSet>();
                    var dead = damageable.DamagePossessable((int)mySkills.primaryEffectAmount, victim, skillManager, cartelTutorial);
                    if (dead)
                    {
                        Debug.Log(mySkills.primarySkillType + " successfully defeated " + victim.gameObject.name);
                        //skillManager.Possess(victim, victim.creatureAppearance);
                    }
                    //else print("no murio");
                }
            }
        }

        yield return new WaitForSeconds(0.2f);
        movement.anim.attacked = false;
        yield return new WaitForSeconds(mySkills.primaryCooldown - 0.2f);
        //hitbox.SetActive(false);
        //hitbox2.SetActive(false);
        manager.SetColliderAction(mySkills, false);
        mySkills.primaryHasExecuted = false;
    }

    public void DefineSkills(CharacterSkillSet mySkills)
    {
        mySkills.primaryExecute = (manager) =>
        {
            movement.anim.attacked2 = false;

            // Agregar esta linea a cualquier acción que realice algo al colisionar (seteando primary o secondary)
            // y despues agregarla con el bool en false cuando la habilidad termine
            manager.SetColliderAction(mySkills, true, SkillSlot.primary);

            if (Time.time > mySkills.primaryExecTime && mySkills.primaryHasExecuted == false)
            {
                mySkills.primaryHasExecuted = true;
                //Debug.Log("Attacked");
                movement.anim.attacked = true;
                //hitbox.SetActive(true);
                manager.StartCoroutine(AttackEnemy(manager));
                mySkills.primaryExecTime = Time.time + mySkills.primaryCooldown;
            }
            else if (mySkills.primaryHasExecuted == true)
            {
                //Debug.Log("Attacked2");
                movement.anim.attacked2 = true;
                //hitbox2.SetActive(true);
                //hitbox.SetActive(false);
                manager.StopCoroutine(AttackEnemy(manager));
                manager.StartCoroutine(AttackEnemy(manager));
                mySkills.primaryHasExecuted = false;
                movement.anim.attacked = false;
            };
        };

        mySkills.secondaryExecute = (manager) =>
        {

        };

    }
}
