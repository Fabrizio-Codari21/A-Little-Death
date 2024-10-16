using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PossessableHealth : Health
{
    [HideInInspector] public bool canBePossessed = false;
    CharacterSkillSet _victim;
    PlayerSkillManager _skillManager;
    [SerializeField] Animator _animator;
    [SerializeField] float possesionTime;
    GameObject _cartelTutorial;
    [SerializeField] GameObject estoEsTemporalHayQueBorrarlo;

    public float stunTime;
    

    public override bool Damage(GameObject damager, int damage)
    {
        Debug.Log(this + "took damage");
        currentHealth -= damage;
        KnockBack(damager, damage, currentHealth <= 0 ? false : true); 

        if (currentHealth <= 0)
        {
            if(estoEsTemporalHayQueBorrarlo != null) { estoEsTemporalHayQueBorrarlo.SetActive(true); }
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
        return Damage(skillManager.gameObject, damage);
    }

    IEnumerator Possessable()
    {
        GetComponent<EntityMovement>().canMove = false;  
        
        if(_animator) _animator.SetTrigger("Stunned");
        if(_cartelTutorial != default) { _cartelTutorial.SetActive(true); }
        canBePossessed = true;
        this.WaitAndThen(0.25f, () =>
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        });

        yield return new WaitForSeconds(stunTime);

        if (_cartelTutorial != default) { Destroy(_cartelTutorial); }
        print("murio");
        Die();
    }

    public virtual void Update()
    {
        if (canBePossessed && this.Inputs(MyInputs.Possess))
        {
            if(Vector2.Distance
              (a: new Vector2(transform.position.x, transform.position.y), 
              b: new Vector2(_skillManager.transform.position.x, _skillManager.transform.position.y)) 
              <= _skillManager.possessingRange)
            {
                _skillManager.Possess(_victim, _victim.creatureAppearance, this.possesionTime);
                if (_cartelTutorial != default) { Destroy(_cartelTutorial); }
            }

        }
    }
}
