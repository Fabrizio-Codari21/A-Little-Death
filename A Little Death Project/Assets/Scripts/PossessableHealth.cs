using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PossessableHealth : Health
{
    bool canBePossessed = false;
    CharacterSkillSet _victim;
    PlayerSkillManager _skillManager;
    [SerializeField] Animator _animator;
    [SerializeField] float possesionTime;
    GameObject _cartelTutorial;

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

    public bool DamagePossessable(int damage, CharacterSkillSet victim, PlayerSkillManager skillManager, GameObject cartelTutorial = default)
    {
        _victim = victim;
        _skillManager = skillManager;
        _cartelTutorial = cartelTutorial;
        return Damage(damage);
    }

    IEnumerator Possessable()
    {
        GetComponent<EnemyMovement>().canMove = false;
        _animator.SetTrigger("Stunned");
        if(_cartelTutorial != default) { _cartelTutorial.SetActive(true); }
        canBePossessed = true;
        yield return new WaitForSeconds(stunTime);
        if (_cartelTutorial != default) { Destroy(_cartelTutorial); }
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
                _skillManager.Possess(_victim, _victim.creatureAppearance, possesionTime);
                if (_cartelTutorial != default) { Destroy(_cartelTutorial); }
                Die();
            }

        }
    }
}
