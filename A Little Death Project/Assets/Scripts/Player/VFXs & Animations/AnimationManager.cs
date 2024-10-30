using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool jumped = false;
    public bool attacked = false;
    public bool attacked2 = false;
    [SerializeField] ThaniaMovement thania;
    [SerializeField] JumpManager thaniaJ;
    [SerializeField] ThaniaSkills thaniaSkills;
    [SerializeField] PlayerSkillManager manager;
    public Animator animator;
    public bool attackEnded = true;

    private void Start()
    {
        thania = GetComponentInParent<ThaniaMovement>();
        thaniaJ = GetComponentInParent<JumpManager>();
        manager = GetComponentInParent<PlayerSkillManager>();
        thaniaSkills = GetComponentInParent<ThaniaSkills>();
    }

    void OnEnable()
    {
//<<<<<<< HEAD
        jumped = false;
        attacked = false;
        attacked2 = false;
        attackEnded = true;
//=======
        if(thania == null) thania = GetComponentInParent<ThaniaMovement>();
        if (manager == null) manager = GetComponentInParent<PlayerSkillManager>();
        if (thaniaSkills == null) thaniaSkills = GetComponentInParent<ThaniaSkills>();
        if (thaniaH == null) thaniaH = GetComponentInParent<ThaniaHealth>();

        jumped = false;
        attacked = false;
        thaniaH.sRenderer = GetComponent<SpriteRenderer>();
//>>>>>>> parent of 72cd24f (Fixeos Colores)
    }

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(thania.rb.velocity.x));
        animator.SetBool("Jump", jumped);
        animator.SetBool("Attack", attacked);
        animator.SetBool("Attack2", attacked2);
        animator.SetBool("AttackAnimEnded", attackEnded);
    }

    public  void AttackAnimEnd()
    {
        attackEnded = true;
    }

    public void AttackAnimStart()
    {
        attackEnded = false;
    }
    
    public void Attack2AnimEnd()
    {
        attacked2 = false;
    }

    public void AttackEnd()
    {
        attacked = false;
    }

    public void SetAttackTrigger()
    {
        thaniaSkills.Attack(manager);
    }
}
