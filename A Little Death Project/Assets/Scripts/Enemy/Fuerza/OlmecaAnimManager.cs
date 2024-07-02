using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlmecaAnimManager : MonoBehaviour
{
    public bool attacked = false;
    public bool canSeePlayer = false;
    public bool landed = false;
    [SerializeField] OlmecaAttack olmeca;
    public Animator animator;

    private void Update()
    {
        animator.SetBool("Attacked", attacked);
        animator.SetBool("PlayerOnView", canSeePlayer);
        animator.SetBool("JustLanded", landed);
    }
}
