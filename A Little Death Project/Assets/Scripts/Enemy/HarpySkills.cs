using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpySkills : MonoBehaviour, ISkillDefiner
{
    public JumpManager jumpManager;
    public CharacterSkillSet mySkills;
    public ThaniaMovement movement;
    public GameObject attack;

    private void Awake()
    {
        DefineSkills(mySkills);
    }

    public void DefineSkills(CharacterSkillSet mySkills)
    {
        mySkills.primaryExecute = (manager) =>
        {
            //movement.anim.attacked2 = false;

            //// Agregar esta linea a cualquier acción que realice algo al colisionar (seteando primary o secondary)
            //// y despues agregarla con el bool en false cuando la habilidad termine
            //manager.SetColliderAction(mySkills, true, SkillSlot.primary);

            //IEnumerator AttackEnemy(PlayerSkillManager manager)
            //{
            //    if (mySkills.primaryHasExecuted)
            //    {

            //        var hits = Physics2D.CircleCast(manager.sk.skills[0].origin.position,
            //                                        manager.sk.skills[0].distance,
            //                                        transform.right,
            //                                        0f,
            //                                        manager.sk.skills[0].validLayer);

            //        attack.SetActive(true);
            //        if (hits != false && manager.GetColliderAction() == ColliderAction.Damage)
            //        {
            //            Debug.Log($"{manager.sk.skills[0].skillType} dealt {manager.sk.skills[0].effectAmount} damage");

            //            PossessableHealth damageable = hits.collider.gameObject.GetComponent<PossessableHealth>();

            //            if (damageable != null)
            //            {
            //                var victim = hits.collider.gameObject.GetComponent<CharacterSkillSet>();
            //                var dead = damageable.DamagePossessable((int)manager.sk.skills[0].effectAmount, victim, manager);
            //                if (dead)
            //                {
            //                    Debug.Log(manager.sk.skills[0].skillType + " successfully defeated " + victim.gameObject.name);
            //                    //skillManager.Possess(victim, victim.creatureAppearance);
            //                }
            //                //else print("no murio");
            //            }
            //        }
            //        else print("no hay ataque");
            //    }

            //    yield return new WaitForSeconds(0.2f);
            //    movement.anim.attacked = false;
            //    attack.SetActive(false);
            //    yield return new WaitForSeconds(manager.sk.skills[0].cooldown - 0.2f);
            //    //hitbox.SetActive(false);
            //    //hitbox2.SetActive(false);
            //    manager.SetColliderAction(mySkills, false);
            //    manager.sk.skills[0].executed = false;
            //}

            //if (Time.time > mySkills.primaryExecTime && mySkills.primaryHasExecuted == false)
            //{
            //    mySkills.primaryHasExecuted = true;
            //    //Debug.Log("Attacked");
            //    movement.anim.attacked = true;
            //    //hitbox.SetActive(true);
            //    manager.StartCoroutine(AttackEnemy(manager));
            //    mySkills.primaryExecTime = Time.time + mySkills.primaryCooldown;
            //}
            //else if (mySkills.primaryHasExecuted == true)
            //{
            //    //Debug.Log("Attacked2");
            //    movement.anim.attacked2 = true;
            //    //hitbox2.SetActive(true);
            //    //hitbox.SetActive(false);
            //    manager.StopCoroutine(AttackEnemy(manager));
            //    manager.StartCoroutine(AttackEnemy(manager));
            //    mySkills.primaryHasExecuted = false;
            //    movement.anim.attacked = false;
            //};
        };

        mySkills.secondaryExecute = (manager) =>
        {
            jumpManager.dJump = true;
            if (Time.timeScale > 0) jumpManager.DoubleJump();
        };
    }
}
