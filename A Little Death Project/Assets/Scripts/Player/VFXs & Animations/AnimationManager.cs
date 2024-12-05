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
    public Animator baseFormAnimator;
    public bool attackEnded = true;
    public Color OriginalColor;
    public GameObject gorgonPivot;

    private void Awake()
    {
        OriginalColor = this.GetComponent<SpriteRenderer>().color;
    }

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

        thaniaH.og = OriginalColor;

        jumped = false;
        attacked = false;
        thaniaH.sRenderer = GetComponent<SpriteRenderer>();
        thaniaH.sRenderer.color = thaniaH.og;
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

    void ActivateMovement()
    {
        Debug.Log("Here");
        manager.CanMove();
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
        animator.SetInteger("NextAttack", Random.Range(0, 2)); 
        thaniaSkills.Attack(manager);
    }

    public void EndDesposession()
    {
        manager.EndAnimationDesposession();
    }

    public void DontRoll()
    {
        animator.ResetTrigger("RockStart");
        animator.ResetTrigger("RockEnd");
    }

    public void AimPoison()
    {
        if (gorgonPivot)
        {
            gorgonPivot.SetActive(!gorgonPivot.activeInHierarchy);
        }
    }
}
