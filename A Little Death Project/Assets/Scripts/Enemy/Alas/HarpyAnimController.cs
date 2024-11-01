using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpyAnimController : MonoBehaviour
{
    public bool attacked = false;
    [SerializeField] WingsAttackDetection harpy;
    public Animator animator;
    GameObject parent;
    [SerializeField] Collider2D REFERENCIATEMPORAL;

    private void Update()
    {
        animator.SetBool("Attack", attacked);
    }

    void TEMPORAL()
    {
        REFERENCIATEMPORAL.offset += new Vector2(0f, -5f);
    }

    void TEMPORAL2()
    {
       REFERENCIATEMPORAL.offset += new Vector2(0f, 5f);
    }
}
