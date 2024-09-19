using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool jumped = false;
    public bool attacked = false;
    [SerializeField] ThaniaMovement thania;
    [SerializeField] JumpManager thaniaJ;
    public Animator animator;
    public bool attackEnded = true;

    private void Start()
    {
        thania = GetComponentInParent<ThaniaMovement>();
        thaniaJ = GetComponentInParent<JumpManager>();
    }

    void OnEnable()
    {
        jumped = false;
        attacked = false;
        attackEnded = true;
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
}
