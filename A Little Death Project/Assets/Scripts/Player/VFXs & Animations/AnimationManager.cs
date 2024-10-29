using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool jumped = false;
    public bool attacked = false;
    [SerializeField] ThaniaMovement thania;
    [SerializeField] ThaniaHealth thaniaH;
    [SerializeField] ThaniaSkills thaniaSkills;
    [SerializeField] PlayerSkillManager manager;
    public Animator animator;
    public bool attackEnded = true;

    private void Start()
    {
        thania = GetComponentInParent<ThaniaMovement>();
        manager = GetComponentInParent<PlayerSkillManager>();
        thaniaSkills = GetComponentInParent<ThaniaSkills>();
        thaniaH = GetComponentInParent<ThaniaHealth>();
    }

    void OnEnable()
    {
        if(thania == null) thania = GetComponentInParent<ThaniaMovement>();
        if (manager == null) manager = GetComponentInParent<PlayerSkillManager>();
        if (thaniaSkills == null) thaniaSkills = GetComponentInParent<ThaniaSkills>();
        if (thaniaH == null) thaniaH = GetComponentInParent<ThaniaHealth>();

        jumped = false;
        attacked = false;
        thaniaH.sRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(thania.rb.velocity.x));
        animator.SetBool("Jump", jumped);
        animator.SetBool("Attack", attacked);
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

    public void AttackEnd()
    {
        attacked = false;
    }

    public void SetAttackTrigger()
    {
        thaniaSkills.Attack(manager);
    }
}
