using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PossessableHealth : Health
{
    [HideInInspector] public bool canBePossessed = false;
    CharacterSkillSet _victim;
    public PlayerSkillManager _skillManager;
    [SerializeField] Animator _animator;
    [SerializeField] float possesionTime;
    GameObject _cartelTutorial;
    public Collider2D arpiaGroundCheck;
    [HideInInspector] public bool startedPossession = false;

    public float stunTime;
    IEnumerator _possessable;
    public bool isTutorial;
    [HideInInspector] public bool immune;

    public override bool Damage(GameObject damager, int damage)
    {
        Debug.Log(this + "took damage");
        currentHealth -= damage;
        KnockBack(damager, damage, currentHealth <= 0 ? false : true); 

        if (currentHealth <= 0)
        {
            if(audioManager) audioManager.stunned.Play();
            _possessable = Possessable();
            StartCoroutine(_possessable);
            return true;
        }
        else return false;
    }

    public void ResetHealth() => currentHealth = maxHealth;

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
            //GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().gravityScale = 3;
            if(arpiaGroundCheck != null)
            {
                this.GetComponent<Collider2D>().enabled = false;
                Debug.Log("Se desactivo");
            }
        });

        yield return new WaitForSeconds(stunTime);

        if (_cartelTutorial != default) { Destroy(_cartelTutorial); }
        print("murio");
        Die();
    }

    public override void Die()
    {
        var manager = GetComponent<Spawnable>().parentSpawner.enemyManager;
        var enemy = GetComponent<CharacterSkillSet>();

        manager.GetFromPool(enemy,
                            manager.enemyPools[enemy.creatureAppearance].transform.position,
                            Quaternion.identity,
                            isSpawning: false);

        canBePossessed = false;
        StopCoroutine(_possessable);
        _animator.SetTrigger("Reset");
        GetComponent<Spawnable>().OnDespawn();

        this.GetComponent<Collider2D>().enabled = true;

        currentHealth = maxHealth;
        GetComponent<EntityMovement>().canMove = true;
    }

    public void OnPossess(int x)
    {
        if (isTutorial) TutorialManager.instance.ChangeTutorial(x);
    }

    public virtual void Update()
    {
        if (canBePossessed && this.Inputs(MyInputs.Possess) && startedPossession == false)
        {
            if (_skillManager == null) return;

            if(Vector2.Distance
              (a: new Vector2(transform.position.x, transform.position.y), 
              b: new Vector2(_skillManager.transform.position.x, _skillManager.transform.position.y)) 
              <= _skillManager.possessingRange)
            {
                _animator.SetTrigger("Possessed");
                _skillManager.Possess(_victim, _victim.creatureAppearance, this.possesionTime);
                if (_cartelTutorial != default) { Destroy(_cartelTutorial); }
                startedPossession = true;
                if (GetComponent<Harpy>()) { OnPossess(3); }
                else if (GetComponent<DeerHealth>()) 
                {
                    OnPossess(1);
                    var light = GetComponentInChildren<Light2D>();
                    if(light) { light.enabled = false; }
                }
            }
        }
    }
}
