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
    public Color OriginalColor;

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

    void TEMPORAL()
    {
        GetComponent<Collider2D>().offset += new Vector2(0f, -5f);
    }

    public  void AttackAnimEnd()
    {
        attackEnded = true;
    }

    void ActivateMovement()
    {
        manager.CanMove();
    }

    void TEMPORAL2()
    {
        GetComponent<Collider2D>().offset += new Vector2(0f, 5f); //ESTA LINEA ES TEMPORAL
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
