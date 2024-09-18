using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessableHealth : Health
{
    bool canBePossessed = false;
    CharacterSkillSet _victim;
    PlayerSkillManager _skillManager;

    public float stunTime;
    

    public override bool Damage(int damage)
    {
        Debug.Log(this + "took damage");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StartCoroutine(Possessable());
            return true;
        }
        else return false;
    }

    public bool DamagePossessable(int damage, CharacterSkillSet victim, PlayerSkillManager skillManager)
    {
        _victim = victim;
        _skillManager = skillManager;
        return Damage(damage);
    }

    IEnumerator Possessable()
    {
        GetComponent<EnemyMovement>().canMove = false;
        // Aca cambiaria la animacion y todo eso
        canBePossessed = true;
        yield return new WaitForSeconds(stunTime);
        Die();
    }

    public virtual void Update()
    {
        if(canBePossessed && Input.GetKeyDown(KeyCode.Q))
        {
            if(Vector2.Distance
              (a: new Vector2(transform.position.x, transform.position.y), 
              b: new Vector2(_skillManager.transform.position.x, _skillManager.transform.position.y)) 
              <= _skillManager.possessingRange)
            {
                _skillManager.Possess(_victim, _victim.creatureAppearance);
                Die();
            }

        }
    }
}
