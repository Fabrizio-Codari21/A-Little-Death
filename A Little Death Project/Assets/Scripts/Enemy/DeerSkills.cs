using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerSkills : MonoBehaviour, ISkillDefiner
{
    public CharacterSkillSet mySkills;
    public ParticleSystem VFX;
    public AudioSource SFX;

    public void DefineSkills(CharacterSkillSet mySkills)
    {
        Debug.Log("Define secondary: " + mySkills.secondarySkillType);
        mySkills.primaryExecute = (manager) =>
        {

        };

        // Probar de hacer que las acciones reciban el GameObject del jugador como parametro
        mySkills.secondaryExecute = (manager) =>
        {

            if (manager != null && !mySkills.secondaryHasExecuted)
            {
                manager.SetColliderAction(mySkills, true, SkillSlot.secondary);

                IEnumerator Dash()
                {
                    print("Dash");
                    var rb = mySkills.secondaryOrigin.GetComponent<Rigidbody2D>();
                    var move = rb.gameObject.GetComponent<ThaniaMovement>();

                    move.anim.attacked = true;
                    mySkills.secondaryHasExecuted = true;
                    move.isDashing = true;

                    float originalGravity = rb.gravityScale;
                    rb.gravityScale = 0f;
                    rb.velocity = new Vector2(mySkills.secondaryOrigin.localScale.x * mySkills.secondaryDistance, 0f);

                    yield return new WaitForSeconds(0.3f);

                    rb.gravityScale = originalGravity;
                    manager.SetColliderAction(mySkills, false);
                    move.isDashing = false;

                    yield return new WaitForSeconds(mySkills.secondaryCooldown);

                    move.anim.attacked = false;
                    mySkills.secondaryHasExecuted = false;
                    // Debug.Log("Termino Dash");
                }

                if (VFX) VFX.Play();
                if (SFX) SFX.Play();
                manager.StartCoroutine(Dash());
            }
        };
    }

    void Awake()
    {
        DefineSkills(mySkills);
    }
}
