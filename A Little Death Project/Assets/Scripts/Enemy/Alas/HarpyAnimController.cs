using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpyAnimController : MonoBehaviour
{
    public bool attacked = false;
    [SerializeField] WingsAttackDetection harpy;
    public Animator animator;
    GameObject parent;

    private void Update()
    {
        animator.SetBool("Attack", attacked);
    }
}
